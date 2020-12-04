# convert-string-problem
A test application to create a heirarchical list from a flat string with nested parentheses

The goal is to take this input:
id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)

And result in this output: 

- id
- name
- email
- type
  - id
  - name
  - customFields
    - c1
    - c2
    - c3
- externalId

And also to this output:

- email
- externalId
- id
- name
- type
  - customFields
    - c1
    - c2
    - c3
  - id
  - name
  
