import torch
from torchvision import datasets, transforms
import matplotlib.pyplot as plt

import helper
import clothes_load_usage as cls

# Define a transform to normalize the data
transform = transforms.Compose([transforms.ToTensor(),
                                transforms.Normalize((0.5,), (0.5,))])

# Download and load the test data
testset = datasets.FashionMNIST('~/.pytorch/F_MNIST_data/', download=True, train=False, transform=transform)
testloader = torch.utils.data.DataLoader(testset, batch_size=64, shuffle=True)

# Load network model
model = cls.load_checkpoint('checkpoint.pth')
print(model)

dataiter = iter(testloader)
for _ in range (len(testloader)):
    images, labels = dataiter.next()
    img = images[0]
    # Convert 2D image to 1D vector
    img = img.resize_(1, 784)

    # Calculate the class probabilities (softmax) for img
    ps = torch.exp(model(img))

    # Plot the image and probabilities
    helper.view_classify(img.resize_(1, 28, 28), ps, version='Fashion')
    plt.show()
