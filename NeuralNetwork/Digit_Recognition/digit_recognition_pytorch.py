import helper
import torch
from torch import nn
from torch import optim
import torch.nn.functional as F
from torchvision import datasets, transforms

# Define a transform to normalize the data
transform = transforms.Compose([transforms.ToTensor(),
                                transforms.Normalize((0.5,), 
				(0.5,)),
                              ])

# Download the training data
trainset = datasets.MNIST('~/.pytorch/MNIST_data/', download=True, train=True, transform=transform)
# Load the training data
trainloader = torch.utils.data.DataLoader(trainset, batch_size=64, shuffle=True)

# Neural Network model
model = nn.Sequential(nn.Linear(784, 128),  # First layer get as input 784 pixel and give counted 128 outputs
                      nn.ReLU(),  # First layer use rectifier (ReLU) activation function (it's quicker)
                      nn.Linear(128, 64),  # Second layer get as input 128 scores from first layer and give counted 64 outputs
                      nn.ReLU(),  # Second layer use rectifier (ReLU) activation function (it's quicker)
                      nn.Linear(64, 10),  # Output layer get as input 64 scores from second layer and give 10 outputs (probabilities for each digit)
                      nn.LogSoftmax(dim=1))  # Output layer use softmax activation function to calculate the probability 

# Define loss function
criterion = nn.NLLLoss()
# Define optimizer with parameters to optimize and a learning rate
optimizer = optim.SGD(model.parameters(), lr=0.003)

# How many times we repaet the process
epochs = 5

# Learning process
for e in range(epochs):
	# Initial value of loss for each image
	running_loss = 0
	# For each image in data set, train neural network model
	for images, labels in trainloader:
		# Flatten MNIST images into a 784 long vector
	        images = images.view(images.shape[0], -1)
    
	        # Training pass
	        optimizer.zero_grad()
        	# Calculating output
	        output = model(images)
		# Calcualting loss
	        loss = criterion(output, labels)  # Note: output is predicted output and  labels is our expected output
		# Calculating backpropagation based on loss
	        loss.backward()
		# Update weights and biases
	        optimizer.step()
        	# Calculating loss for epoch
	        running_loss += loss.item()
	# Print training loss for epoch
	else:
        	print(f"Training loss: {running_loss/len(trainloader)}")

#---
# Usage of our trained model
#---
images, labels = next(iter(trainloader))

img = images[0].view(1, 784)
# Turn off gradients to speed up this part
with torch.no_grad():
    logps = model(img)

# Output of the network are log-probabilities, need to take exponential for probabilities
ps = torch.exp(logps)
helper.view_classify(img.view(1, 28, 28), ps)

