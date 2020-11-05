import numpy as np

np.random.seed(0)

X = [[1, 2, 3, 2.5],
     [2.0, 5.0, -1.0, 2.0],
     [-1.5, 2.7, 3.3, -0.8]]


class LayerDense:
    def __init__(self, n_inputs, n_neurons):
        multiplication_weights_factor = 0.10
        self.weights = multiplication_weights_factor * np.random.randn(n_inputs, n_neurons)
        self.biases = np.zeros((1, n_neurons))

        self.output = []

    def forward(self, inputs):
        self.output = np.dot(inputs, self.weights) + self.biases


if __name__ == '__main__':
    layer1 = LayerDense(4, 5)
    layer2 = LayerDense(5, 2)

    layer1.forward(X)
    print(f"Layer 1: \n{layer1.output}\n\t---")
    layer2.forward(layer1.output)
    print(f"Layer 2: \n{layer2.output}\n\t---")
