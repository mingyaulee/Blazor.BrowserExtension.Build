# Contributing to this repository

## We Develop with Github
We use GitHub to host code, to track issues and feature requests, as well as accept pull requests.

## Report bugs using Github's issues
We use GitHub issues to track bugs. Report a bug by opening a new issue.

## Write bug reports with detail, background, and sample code
Great Bug Reports tend to have:

- A quick summary and/or background
- Steps to reproduce
  - Be specific!
  - Give sample code if you can.
- What you expected would happen
- What actually happens
- Notes (possibly including why you think this might be happening, or what you have tried that didn't work)

## We Use Github Flow, So All Code Changes Happen Through Pull Requests
Pull requests are the best way to propose changes to the codebase. We actively welcome your pull requests:

1. Fork the repo and create your branch from main.
0. If you've added code that should be tested, add tests.
0. Ensure the test suite passes.
0. Create a pull request!

## Working in this repository
This repository ships the `Blazor.BrowserExtension.Build` NuGet package. It contains the MSBuild tasks and targets for building and publishing browser extensions, along with the Roslyn analyzer/source generator packaged with it.

The test projects cover the analyzer and build package. Build integration tests use a local Blazor app and test-only runtime stubs to exercise the build package without depending on the runtime package.

## License
By contributing, you agree that your contributions will be licensed under its MIT License.