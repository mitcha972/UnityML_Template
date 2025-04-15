# 🧠 Unity ML Template


## Overview

**Unity ML Template** is a clean and customizable starting point for building machine learning environments using [Unity ML-Agents](https://unity.com/products/machine-learning-agents). Whether you're prototyping a new environment, training agents in a custom simulation, or exploring reinforcement learning, this template provides a ready-to-use Unity project structure, integrated ML-Agents setup, and helpful baseline components.

> **Compatible with ML-Agents Release 19+**

## Table of Contents

1. [Getting Started](#getting-started)  
2. [Environment Setup](#environment-setup)  
3. [Training Agents](#training-agents)

---

## Getting Started

1. Install the [Unity ML-Agents Toolkit](https://github.com/Unity-Technologies/ml-agents) (Release 19 or later).
2. Clone or download this repository.
3. Open the Unity project via Unity Hub:  
   `Unity Hub → Projects → Add → Select the root folder`.
4. Set up a Python virtual environment (3.10):  
   ```bash
   python3.10 -m venv venv
   ```
5. Activate the Env 
   ```bash
   \venv\Scripts\activate
   ```
6. Add PyTorch Package
   ```bash
   pip3 install torch~=2.2.1 --index-url https://download.pytorch.org/whl/cu121
   ```
7. Add PyTorch Package
   ```bash
   python -m pip install mlagents==1.1.0
   ```
8. Add PyTorch Package
   ```bash
   pip install tensorboard
   ```
9. Check the required Python packages:  
   Follow the installation guide here:  
   https://unity-technologies.github.io/ml-agents/Installation/

---

## Environment Setup
https://unity-technologies.github.io/ml-agents/Getting-Started/
- Open Unity and load the project.
- Make sure ML-Agents package is properly imported.

---

## Training Agents

1. Activate your virtual environment (where `ml-agents` is installed).
2. Create or modify a YAML config file for training (see the `config/` folder for examples).
3. Run the training command:  
   ```bash
   mlagents-learn <path-to-config> --run-id=<your_run_id> --time-scale=1
   mlagents-learn config\ppo\<file-name>.yaml --run-id=<your_run_id> --time-scale=1
   ```
4. You can rerive the training data after by running commad
   ```bash
   tensorboard --logdir results/your_run_id
   ```
   https://unity-technologies.github.io/ml-agents/Learning-Environment-Examples/
