# Nodexr

[![Test and Deploy](https://github.com/Jcparkyn/nodexr/actions/workflows/main.yml/badge.svg)](https://github.com/Jcparkyn/nodexr/actions/workflows/main.yml)
[![License](https://img.shields.io/github/license/Jcparkyn/nodexr)](https://github.com/Jcparkyn/nodexr/blob/master/LICENSE)
[![GitHub issues](https://img.shields.io/github/issues/Jcparkyn/nodexr)](https://github.com/Jcparkyn/nodexr/issues)

Nodexr is a node-based Regular Expression editor, created in C# and Blazor.
## Try it at [nodexr.net](https://www.nodexr.net)

You can also try it out by starting with one of these example expressions:
* [Floating point with exponent](https://www.nodexr.net/?parse=%5Cb%5B-%2B%5D%3F%5Cd*%5C.%3F%5Cd%2B%28%5BeE%5D%5B-%2B%5D%3F%5Cd%2B%29%3F%5Cb&search=123.45,%201563,%0A9.76456e12,%201E-9,%200.5e-7&replace=%5B$0,%20$1%5D)
* [Email address (simple)](https://www.nodexr.net/?parse=%5Cb%28%5Cw%2B%28%3F%3A%5B-%2B.%27%5D%5Cw%2B%29*%29@%5Cw%2B%28%3F%3A%5B-.%5D%5Cw%2B%29*%5C.%5Cw%2B%28%3F%3A%5B-.%5D%5Cw%2B%29*%5Cb&search=not-an-email@email.com%0Afake.person%2B666@nodexr.net&replace=$1)
* [Email address (advanced)](https://www.nodexr.net/?parse=%5Cb%28%3F%28%22%29%22.%2B%3F%28%3F%3C!%5C%5C%29%22@%7C%28%5B0-9a-z%5D%28%28%3F%3A%5C.%28%3F!%5C.%29%29%7C%5B-!%23%5C$%25%26%27%5C*%5C%2B%2F%3D%5C%3F%5C%5E%60%5C%7B%5C%7D%5C%7C~%5Cw%5D%29*%29%28%3F%3C%3D%5B0-9a-z%5D%29@%29%28%3F%28%5C%5B%29%5C%5B%28%3F%3A%5Cd%7B1,3%7D%5C.%29%7B3%7D%5Cd%7B1,3%7D%5C%5D%7C%28%3F%3A%5B0-9a-z%5D%5B-%5Cw%5D*%5B0-9a-z%5D*%5C.%29%2B%5Ba-z0-9%5D%5B%5C-a-z0-9%5D%7B0,22%7D%5Ba-z0-9%5D%29%5Cb&search=not-an-email@email.com%0Afake.person%2B666@nodexr.net&replace=$1)
* [URL](https://www.nodexr.net/?parse=%5Cb%28https%3F%3A%5C%2F%5C%2F%29%3F%28www%5C.%29%3F%5B-a-zA-Z0-9@%3A%25._%5C%2B~%23%3D%5D%7B2,256%7D%5C.%5Ba-z%5D%7B2,6%7D%5Cb%28%5B-a-zA-Z0-9@%3A%25_%5C%2B.~%23%3F%26%2F%2F%3D%5D*%29%5Cb&search=https%3A%2F%2Fwww.nodexr.net%0Anodexr.net%0Awww.github.com%2FJcparkyn%2Fnodexr&replace=%5BLink%5D%28$0%29)

This screenshot shows a Regular Expression used to match floating point numbers, with or without an exponent:

![Screenshot](https://github.com/Jcparkyn/nodexr/blob/dev/Nodexr/images/Screenshot_floatingPoint_2.png?raw=true)

## Features
* Uses the full .NET Regex engine for search and replace (unlike most online Regex tools).
* Show the results of search and replace (using the .NET Regex engine) in the browser in realtime.
* Full syntax highlighting.
* Hover over sections of the output to see which node they were generated by.
* The nodes can be used to work with (almost) all of the .NET Regex spec, and any additional functionality can be implemented either by using the *Text* node without escaping, or with a custom *Group* node.
* The node-based approach makes it almost impossible to have syntax errors, missing parentheses etc (except with certain nodes)
* Enter an existing Regex and it will be parsed into a fully editable node tree (using the *Edit* button next to the output). This should work with expressions of any complexity level (although there are a couple of niche features that can't yet be parsed properly).
* Create a shareable link for your expression, to send to someone else or come back to later (this feature currently relies on the expression parsing, so in some cases the node tree will be a little different after sharing - but the expression should be the same).
* Information about each node can be found by clicking the **(i)** button next to its title.
* Automatically deals with non-capturing groups, so you no longer have to think about them in 99% of cases.
* Runs completely client-side - no communication with a server after the initial page load.

## How To Use
Drag-and-drop nodes from the left panel to insert them into the main window. The final result/output of your nodes must be connected to the _Output_ node and is displayed at the top left.

The main concept is that the "nesting" behaviour of regex is expressed by connecting one node to the input of another, but items in sequence are connected using the *Previous* input at the top left of each node. Expressions can alternatively be connected in sequence (concatenated) using the *Concatenate* node.
The output expression will be empty unless one or more nodes are connected to the _Output_ node.

## Nodes
Information about each node can be found by clicking the **(i)** button next to its title.

To use any Regex functionality that cannot be implemented with the provided nodes, create a *Text* node with 'escape' disabled to input parts of the expression manually.

## Replacement
Use the bottom 3 panels to test a string for searching and/or replacement. Any valid .NET Regex replacement string can be used here, including named and/or numbered group references. The bottom right panel shows the result after replacement.

## Contributing
Any contributions are welcome, but ideally start by creating an [issue](https://github.com/Jcparkyn/nodexr/issues).

## Also check out
A very similar tool called [Regex Nodes](https://github.com/johannesvollmer/regex-nodes) has been made by Johannes Vollmer. There is no connection between Nodexr and Regex Nodes, but it is a very polished alternative for those that need JavaScript regular expressions.
