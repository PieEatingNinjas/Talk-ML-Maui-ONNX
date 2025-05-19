---
theme: quantum
layout: section
transition: fadeIn
---

# Machine Learning

```mermaid
graph TD
    ML1[Example Inputs]
    ML2[Example Outputs]
    ML1 --> ML3[Learning Algorithm]
    ML2 --> ML3
    ML3 --> ML4[Model]
    NewInput[Input] --> ML4
    ML4 --> ML5[Output]
```