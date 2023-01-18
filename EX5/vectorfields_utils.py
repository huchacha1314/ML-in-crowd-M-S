import numpy as np
import matplotlib.pyplot as plt
from scipy.spatial.distance import cdist

from utils import *
from scipy.integrate import solve_ivp


def linear_approximation(t,y,A):
    """
    function for solve_ivp to return vector field of a point
    :param t: for solve_ivp
    :param y: point
    :param A: approximated matrix
    :return:
    """
    return A @ y


def nonlinear_apporximation(t , y, bases, epsilon, C):
    """

    :param t: for solve_ivp
    :param y:
    :param bases:
    :param epsilon:
    :param C: ceofficants
    :return:
    """
    y = y.reshape(1, y.shape[-1])
    x = np.exp(-cdist(y, bases)**2 / epsilon ** 2)
    return x @ C


def plot_phase_portrait(A, p, linear=True, funct=None, args=None):
    """
    Plots a linear vector field in a streamplot, defined with X and Y coordinates and the matrix A.
    :param A: matrix
    :param p: witdh of grid to plot (assumption: width and height are the same)
    :return:
    """
    Y, X = np.mgrid[-p:p:100j, -p:p:100j]
    if linear:
        UV = A@ np.row_stack([X.ravel(), Y.ravel()])
        U = UV[0,:].reshape(X.shape)
        V = UV[1,:].reshape(X.shape)
    else:
        U = []
        V = []
        for x1 in X[0]:
            for x0 in Y[:,0]:
                res = funct(0, np.array([x0,x1]), *args)
                U.append(res[0][0])
                V.append(res[0][1])
        U = np.reshape(U,X.shape)
        V = np.reshape(V, X.shape)
    fig = plt.figure(figsize=(12, 12))

    #  Varying density along a streamline
    plt.streamplot(X, Y, U, V, density=1.0)
    return plt


def rbf(x,x_l,eps):
    """
    computes the radial basis function
    :param x:
    :param x_l:
    :param eps:
    :return:
    """
    return np.exp(-cdist(x, x_l) ** 2 / eps **2)


def approx_vector_field(x0, x1, delta_t, t_end, linear=True, L=100, epsilon=0.1):
    """
    approximates a vector field linear or non-linear with radial basis functions
    :param epsilon:
    :param L: amount of bases in the non-linear approach
    :param linear: bool of linear or non-linear should be used (when non-linear, L and epsilon should be set)
    :param x0: base dataset
    :param x1: target dataset
    :param delta_t:
    :param t_end: to what value a the lowest mse should be searched
    :return: the best approximation of x1, the according mse and delta_t
    """
    # approximate vector
    v = (x1 - x0) / delta_t
    args = []
    # approximate A with the function
    if linear:
        A, _, _, _ = np.linalg.lstsq(x0, v, rcond=10 ^ (-5))
    else:
        x_l = x0[np.random.choice(range(x0.shape[0]), replace=False, size=L)]
        bases = np.exp(-cdist(x0, x_l) ** 2 / epsilon ** 2)
        coef, _, _, _ = np.linalg.lstsq(a=bases, b=v, rcond=1e-5)
        args = [x_l, epsilon, coef]

    best_delta_t = -1
    best_mse = 10000
    x1_approx = []
    solutions = []


    t = np.linspace(0, t_end, 1000)

    for i in range(len(x0)):
        if linear:
            solution = solve_ivp(linear_approximation, [0, t_end], x0[i], args=[A], t_eval=t)
        else:
            solution = solve_ivp(nonlinear_apporximation, [0, t_end], x0[i], args=args, t_eval=t)
        x1_approx.append([solution.y[0, -1], solution.y[1, -1]])
        solutions.append(solution.y)

    for i in range(len(t)):
        approx = [[solutions[j][0][i], solutions[j][1][i]] for j in range(len(solutions))]
        mse = (np.linalg.norm(approx - x1)**2) /2000
        if mse < best_mse:
            x1_approx = approx
            best_mse = mse
            best_delta_t = t[i]

    return x1_approx, best_delta_t, best_mse, args


def plot_x1_approx(x1_x, x1_y, x1_approx):
    """
    plots the approximated x1 against the the target
    :param x1_x: x-value of the target
    :param x1_y: y-value of the target
    :param x1_approx: the approximation
    :return:
    """
    fig = plt.figure(figsize=(10, 10))
    _ = plt.scatter(x1_x, x1_y, color='orange', s=10, label="x1")
    _ = plt.scatter(x1_approx[:, 0], x1_approx[:, 1], color='b', s=10, label="approximated x1")
    _ = plt.legend()
    #_ = plt.savefig("task3_nonlinear_appox")
    _ = plt.show


def solve_system(x0, t_end, args):
    """
    solves the system with the given vector field approximation and plots the steady states.
    :param x0: data
    :param t_end:
    :param args: args for solve_ivp: [x_l, epsilon, coef]
    :return:
    """
    fig = plt.figure(figsize=(10, 10))
    for i in range(len(x0)):
        solution = solve_ivp(nonlinear_apporximation, [0, t_end], x0[i], args=args)
        _ = plt.scatter(solution.y[0, -1], solution.y[1, -1])
    _ = plt.ylim(-4.5, 4.5)
    _ = plt.xlim(-4.5, 4.5)
    #_ = plt.savefig("steadystates")
    _ = plt.show()



