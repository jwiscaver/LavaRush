\# Code Review Checklist



Before returning code, the agent must verify:



Architecture

\- Scripts placed in correct folders

\- No duplicate systems created



Coding Standards

\- PascalCase for public members

\- camelCase for private members

\- Boolean variables read like questions



Unity Rules

\- No GameObject.Find in Update

\- No repeated GetComponent calls

\- References cached in Awake or Start

\- No empty Update methods



Performance

\- Avoid runtime allocations

\- Avoid LINQ in Update loops

