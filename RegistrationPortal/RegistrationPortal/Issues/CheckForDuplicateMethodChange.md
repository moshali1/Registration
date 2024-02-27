## Change in `CheckForDuplicate` Method to Avoid Deadlock

### Previous Implementation

```csharp
public async Task<bool> CheckForDuplicate(Form form)
{
    return await _httpClient.PostAsJsonAsync("api/form/check-duplicate", form).Result.Content.ReadFromJsonAsync<bool>();
}
```

### New Implementation

```csharp
public async Task<bool> CheckForDuplicate(Form form)
{
    var response = await _httpClient.PostAsJsonAsync("api/form/check-duplicate", form);
    return await response.Content.ReadFromJsonAsync<bool>();
}
```

### Issue
The previous implementation was prone to causing deadlocks. This is because it mixed asynchronous and synchronous code improperly by using .Result to synchronously wait for an asynchronous operation to complete.

### How the Issue is Fixed
In the new implementation, the await keyword is used correctly to ensure that the method operates asynchronously from start to finish. This allows the .NET runtime to manage the asynchronous operation, thus freeing up the current thread and making the application more scalable and responsive.

By separating the operation into two distinct await expressions, we allow the framework to handle the asynchronous operations, which helps us avoid deadlocks.