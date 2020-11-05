import torch
import clothes_recognition

def load_checkpoint(filepath):
    checkpoint = torch.load(filepath)
    model = clothes_recognition.Network(checkpoint['input_size'],
                             checkpoint['output_size'],
                             checkpoint['hidden_layers'])
    model.load_state_dict(checkpoint['state_dict'])
    
    return model
