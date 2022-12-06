import numpy as np
from scipy.spatial import KDTree
from sklearn.metrics.pairwise import euclidean_distances


class DiffusionMap:
    """
    class to build a diffusion map out of a given dataset.
    """

    def __init__(self, matrix):
        """
        initializes a Diffusion Map
        :param matrix: dataset
        """
        self.data_matrix = matrix
        self.distance_matrix = None
        self.epsilon = None
        self.w_matrix = None
        self.p_matrix = None
        self.p_matrix_inv = None
        self.k_matrix = None
        self.q_matrix = None
        self.q_matrix_inv_sqrt = None
        self.t_matrix = None
        self.eigenvalues = None
        self.eigenvectors = None

    @staticmethod
    def compute_diagonal_normalization_matrix(matrix):
        """
        computes the diagonal normalization matrix.
        :param matrix:
        :return:
        """
        return np.sum(matrix, axis=0)

    def get_eigenvectors(self, L):
        """
        computes the L biggest eigenvalues and returns there according eigenvectors
        :param L: amount of eigenvectors needed
        :return: eigenvectors
        """
        # step 1: form distance matrix
        self.distance_matrix = euclidean_distances(self.data_matrix, self.data_matrix)
        # step 2: compute epsilon
        self.epsilon = 0.05 * np.max(self.distance_matrix)
        # step 3: form kernel matrix 1
        self.w_matrix = np.exp(-np.power(self.distance_matrix, 2) / self.epsilon)
        # step 4: form diag normalization matrix
        self.p_matrix = self.compute_diagonal_normalization_matrix(self.w_matrix)
        # step 5.1 form inverse p_matrix
        self.p_matrix_inv = np.linalg.inv(np.diag(self.p_matrix))
        # step 5.2 Normalize w to from kernel matrix 2
        self.k_matrix = self.p_matrix_inv @ self.w_matrix @ self.p_matrix_inv
        # step 6 form diagonal normalization matrix
        self.q_matrix = self.compute_diagonal_normalization_matrix(self.k_matrix)
        # step 7.1 form inverse sqrt of matrix q
        self.q_matrix_inv_sqrt = np.diag(np.power(self.q_matrix, -(1 / 2)))
        # step 7.3 form matrix T
        self.t_matrix = self.q_matrix_inv_sqrt @ self.k_matrix @ self.q_matrix_inv_sqrt

        # compute eigenvectors
        self.eigenvalues, self.eigenvectors = np.linalg.eigh(self.t_matrix)
        v_l = self.eigenvectors[:, -L - 1:][:, ::-1]
        v_l = self.q_matrix_inv_sqrt @ v_l
        return v_l
