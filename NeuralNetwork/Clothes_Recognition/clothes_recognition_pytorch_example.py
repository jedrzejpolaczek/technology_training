import torch
from torch import nn
from torch import optim
import torch.nn.functional as F
from torchvision import datasets, transforms
import matplotlib.pyplot as plt
import helper

#---
# GET DATA
#---

# Define a transform to normalize the data
transform = transforms.Compose([transforms.ToTensor(),
                                transforms.Normalize((0.5,), (0.5,))])
# Download and load the training data
trainset = datasets.FashionMNIST('~/.pytorch/F_MNIST_data/', download=True, train=True, transform=transform)
trainloader = torch.utils.data.DataLoader(trainset, batch_size=64, shuffle=True)

# Download and load the test data
testset = datasets.FashionMNIST('~/.pytorch/F_MNIST_data/', download=True, train=False, transform=transform)
testloader = torch.utils.data.DataLoader(testset, batch_size=64, shuffle=True)

#---
# NEURAL NETWORK
#---

#  Define network architecture 
class Network(nn.Module):
	def __init__(self):
		super().__init__()
		self.fc1 = nn.Linear(784, 256)  # First layer as input get 784 pixels
		self.fc2 = nn.Linear(256, 128)
		self.fc3 = nn.Linear(128, 64)
		self.fc4 = nn.Linear(64, 10)  # Output layer give as output 10 classes with probabilities for each

		# Dropout model with 0.2 drop probability
		self.dropout = nn.Dropout(p=0.2)

	def forward(self, x):
		# Make sure input tensor is flattened
		x = x.view(x.shape[0], -1)

		x = self.dropout(F.relu(self.fc1(x)))  # First layer activation function is ReLU (is quick) with droput
		x = self.dropout(F.relu(self.fc2(x)))  # Second layer activation function is ReLU (is quick) with dropout
		x = self.dropout(F.relu(self.fc3(x)))  # Third  layer activation function is ReLU (is quick) with dropout

		# Outout so no dropout
		x = F.log_softmax(self.fc4(x), dim=1)  # Output activation function is softmax to count the probability

		return x

#---
# INIT MODEL
#---

# Create the network
model = Network()
# Define loss function
criterion = nn.NLLLoss()
# Define optimizer with parameters to optimize and a learning rate
optimizer = optim.SGD(model.parameters(), lr=0.003)


#---
# MODEL LEARNIG PROCESS
#---

# How many times we repeat the process
epochs = 30

train_losses = []
test_losses = []

# Training loop
for epoch in range(epochs):
	# Initiate value of loss for each image
	running_loss = 0 
	# For each image in data set, train neural network model
	for images, labels in trainloader:
		# Calculating predicted output
		output = model(images)
		# Calculating loss
		loss = criterion(output, labels)  # Note: labels is expected output

		# Training pass
		optimizer.zero_grad()
		# Calculating backpropagation based on loss
		loss.backward()
		# Update weights and biases
		optimizer.step()

		# Calculating loss for epoch
		running_loss += loss.item()
	#  After each epoch make validation pass
	else:
		# Initiate value of test loss after each epoch
		test_loss = 0
		# Initiate value of accuracy after each epoch
		accuracy = 0

		# Turn off gradients for validation to save memory and computations
		with torch.no_grad():
			# Turn on model evaluation mode to turn off dropout
			model.eval()
			for images, labels in testloader:
				# Calculating test predicted output
				test_output = model(images)
				# Calculating loss
				loss = criterion(test_output, labels)
				
				# Get class probability
				output_class_probability = torch.exp(test_output)
				# Get top highest values of probability for classes
				top_probability, top_class = output_class_probability.topk(1, dim=1)
				# Calculate if classes are correct
				equals = top_class == labels.view(*top_class.shape)
				# Calculating accuracy
				accuracy += torch.mean(equals.type(torch.FloatTensor))

				# Calculating test loss for each epoch
				test_loss +=  loss
		# Turn off evaluation mode and turn on training mode aka tunr on dropout
		model.train()

		# Add train loss for epoch
		train_losses.append(running_loss/len(trainloader))
		# Add test loss for epoch
		test_losses.append(test_loss/len(testloader))
		
		# Print recaption after epoch
		print("Epoch: {}/{}...".format(epoch+1, epochs), end=" ")
		print("Training Loss: {:.3f}..".format(running_loss/len(trainloader)), end=" ")
		print("Test Loss: {:.3f}..".format(test_loss/len(testloader)), end=" ")
		print("Test Accuracy {:.3f}".format(accuracy/len(testloader)))

#---
# USAGE
#---
dataiter = iter(testloader)
images, labels = dataiter.next()
img = images[0]
# Convert 2D image to 1D vector
img = img.resize_(1, 784)

# Calculate the class probabilities (softmax) for img
ps = torch.exp(model(img))

# Plot the image and probabilities
helper.view_classify(img.resize_(1, 28, 28), ps, version='Fashion')
plt.show()

plt.plot(train_losses, label='Training loss')
plt.plot(test_losses, label='Validation loss')
plt.legend(frameon=False)
plt.show()
