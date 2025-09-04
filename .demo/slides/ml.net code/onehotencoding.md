---
theme: quantum
layout: default
transition: fadeIn
---

# One-Hot Encoding

One-hot encoding turns a `categorical value` (like "Cat", "Dog" or "Bird") into a `binary vector`.

```
"Cat"  → [1, 0, 0]
"Dog"  → [0, 1, 0]
"Bird" → [0, 0, 1]
```

If you just assign numbers instead (Cat = 0, Dog = 1, Bird = 2), the model might assume:
- Dog > Cat
- Bird is “close to” Dog
- There’s a numeric relationship between categories (which is wrong)