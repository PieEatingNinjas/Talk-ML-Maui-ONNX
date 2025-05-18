---
theme: quantum
layout: default
transition: fadeIn
---

# Some metrics
- MeanAbsoluteError (MAE): Average absolute difference between the predicted and actual values. 
- MeanSquaredError (MSE): Average of the squared differences between predicted and actual values. 
  -  Squaring the errors penalizes larger errors more heavily, making this metric sensitive to outliers.
- RSquared (R²): Explains the variance in the target variable. 
  - Ranges from 0 to 1, where 1 indicates a perfect fit. 
- RootMeanSquaredError (RMSE): Square root of the MSE
  - Brings the error back to the same scale as the target variable.