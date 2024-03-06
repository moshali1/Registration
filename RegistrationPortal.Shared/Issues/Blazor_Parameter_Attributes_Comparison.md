# Blazor: Comparison Between `[Parameter]` and `[SupplyParameterFromForm]` Attributes

## Introduction

In Blazor, there are different ways to pass and bind data in components. Two of them are the `[Parameter]` and `[SupplyParameterFromForm]` attributes. This document aims to provide an in-depth comparison between these two attributes, clarifying when to use each and why.

## `[Parameter]` Attribute

### Description

The `[Parameter]` attribute is used to define properties in a Blazor component that should receive their values from a parent component.

### Usage

```csharp
[Parameter]
public string Message { get; set; }
```

### Features

- Works in both Blazor WebAssembly and Blazor Server.
- Can be used to pass data from a parent to a child component.
- Supports one-way and two-way data binding.

## `[SupplyParameterFromForm]` Attribute

### Description

The [SupplyParameterFromForm] attribute, introduced in .NET 8, allows server-side Blazor components to bind and validate HTTP form post values directly.

### Usage
```csharp
[SupplyParameterFromForm]
public Movie Movie { get; set; } = new();
```

### Features

- Specific to Blazor Server with server-side rendering mode.
- Automatically binds form post values in HTTP requests to properties in a Blazor component.
- Supports server-side validation using data annotations.

## Key Differences

1. Scope: `[Parameter]` works in both WebAssembly and Server, while [SupplyParameterFromForm] is specific to server-side Blazor.
2. Data Source: `[Parameter]` relies on data passed from parent components, while [SupplyParameterFromForm] binds data directly from HTTP form post requests.
3. Validation: `[SupplyParameterFromForm]` offers built-in server-side validation; for [Parameter], you'd have to handle validation manually or in the parent component.


## Conclusion

Use `[Parameter]` for general component-to-component data flow. Opt for `[SupplyParameterFromForm]` if you need to work specifically with form data in server-side Blazor components and require server-side validation.