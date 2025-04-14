# 🧠 Unity ML Template

![Unity ML](https://www.gocoder.one/static/ultimate-volleyball-eb08a31356cf6a5add9ad2b3ec76cfc6.gif) <!-- You can update this gif if you want a more general one -->

## About

**Unity ML Template** is a clean, customizable starting point for building machine learning environments using [Unity ML-Agents](https://unity.com/products/machine-learning-agents). Whether you're prototyping a new environment, training agents for a custom simulation, or teaching reinforcement learning, this template offers a ready-to-use Unity project structure, integrated ML-Agents setup, and helpful baseline components.

> **Version:** Compatible with ML-Agents Release 19+

## Contents
1. [Getting Started](#getting-started)
2. [Training Agents](#training-agents)
3. [Self-Play Setup](#self-play-setup)
4. [Environment Configuration](#environment-configuration)
5. [Included Baselines](#included-baselines)

## Getting Started

1. Install the [Unity ML-Agents toolkit](https://github.com/Unity-Technologies/ml-agents) (Release 19 or later).
2. Clone or download this repository.
3. Open the Unity project (Unity Hub → Projects → Add → select the root folder).
4. Open your environment scene or use the included example (`VolleyballMain.unity`) as a starting point.
5. Hit ▶️ in the Unity Editor to run the agent in inference mode.

## Training Agents

1. Activate your virtual environment with `ml-agents` installed.
2. Copy or create a YAML config file for training (see `config/` directory for examples).
3. Start training:  
   ```bash
   mlagents-learn <path-to-config> --run-id=<your_run_id> --time-scale=1
