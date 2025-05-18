---
theme: quantum
layout: default
transition: fadeIn
---

# One-Hot Encoding

One-hot encoding turns a `categorical value` (like "male" or "female") into a `binary vector`.

```
"female" → [1, 0]
"male"   → [0, 1]
```


If you convert "female" to 0.0 and "male" to 1.0, the model might assume:
   - "male" is greater than "female"
   - There’s a meaningful numeric relationship between categories