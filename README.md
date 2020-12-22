# AspNetCoreOData NET 5 Cannot write to the response body, the response has completed.Object name: 'HttpResponseStream'


This a sample repo demonstrating an issue with `Microsoft.AspNetCore.OData 8.0.0-preview3`.

When doing parallel requests (which is hard to reproduce), some responses get mixed:
```
  {
    id: 62,
    response: '{"@odata.context":"http://localhost:64149/odata/$metadata#Books(Author())","value":[]}{"@odata.context":"http://localhost:64149/odata/$metadata#Books(Author())","value":[]}'
  }
```
*(notice that the response is text and not valid json)*

and others crash with the error:

```
System.AggregateException: One or more errors occurred. (Cannot write to the response body, the response has completed.
Object name: 'HttpResponseStream'.)
 ---> System.ObjectDisposedException: Cannot write to the response body, the response has completed.
Object name: 'HttpResponseStream'.
```

or

```
System.InvalidOperationException: Reading is already in progress.
   at System.IO.Pipelines.ThrowHelper.ThrowInvalidOperationException_AlreadyReading()
```

Steps to reproduce:
Open the solution
* restore
* build
* run

The sample works with in memory database but you can change it to work with SQL too.

Open the js folder
* `npm install`
* `npm run test`

Notice the output window in Visual Studio. You should see an exception. When you do, some responses will contain invalid json (concatenated results detected by the existence of `}{` sequence).