# DataPrefiller Class

## Overview

The `DataPrefiller` class is a utility class in the `RegistrationPortal.Client.Helpers` namespace. It is designed to prefill a `Form` object with fake data for testing and debugging purposes.

## Features

### Static Data

- Uses a static `List<Form>` named `FakeData` to store predefined sets of fake form data.
- The data is consistent and available throughout the application's runtime.

### Cycling Through Data

- Utilizes a static integer `currentIndex` to keep track of the last-used fake data set.
- Increments `currentIndex` each time `PrefillFields()` is called, cycling back to the start when reaching the end of the list.

### Data Assignment

- The `PrefillFields(Form form)` method updates the properties of the supplied `Form` object with the next set of fake data.

## Limitations

1. **Per-Session Memory**: Data and `currentIndex` are shared across all instances/users within the same session.
2. **Concurrent Access**: Unsynchronized access from multiple threads/users may lead to unpredictable behavior.
3. **Not Persistent**: Data is not saved to any persistent storage and resets when the application is restarted.

## Usage Example

```csharp
DataPrefiller.PrefillFields(form);
```
