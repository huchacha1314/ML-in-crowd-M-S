import numpy as np
import matplotlib.pyplot as plt


def least_squares(x, y):
    """
    takes data x and data y and computes the least-squares-minimization
    :param x:
    :param y:
    :return:
    """
    a = np.vstack([x, np.ones(len(x))]).T
    return np.linalg.lstsq(a, y, rcond=10 ^ (-6))[0]


def plot_linear(x, y, m, c):
    """
    plots the data and its function approximation.
    :param x: data x
    :param y: data y
    :param m:
    :param c:
    :return:
    """
    _ = plt.plot(x, y, 'o', label='Original data', markersize=5)
    _ = plt.plot(x, m * x + c, 'r', label='Fitted line')
    _ = plt.legend()
    #_ = plt.savefig("plot_nonlinear_1")
    plt.show()


def compute_bases(xl, x, epsilon=1):
    """
    computes the radial basis function
    :param xl:
    :param x:
    :param epsilon:
    :return:
    """
    return np.exp(-(x-xl)**2 / (epsilon**2))


def least_squares_radial(x, y, L, epsilon=1):
    """

    :param x data x:
    :param y:data y
    :param L: amount of center points
    :return: function points
    """
    points = np.linspace(np.min(x), np.max(x), L)
    bases = []

    for i in range(L):
        xl = np.ones(len(x))*points[i]
        bases.append(compute_bases(xl, x, epsilon))

    bases = np.array(bases)
    c = np.vstack([bases, np.ones(bases.shape)]).T

    coef = np.linalg.lstsq(c, y, rcond=10e-5)[0]

    result = np.zeros_like(x)
    for i in range(L):
        xl = np.ones(len(x)) * points[i]
        r = compute_bases(xl, x, epsilon) * coef[i]
        result = result + r

    return result, bases
