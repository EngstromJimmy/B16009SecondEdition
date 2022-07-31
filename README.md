# SecondEdition

For tech reviewers (and for me so I dont miss something)
There will be small changes in early chapters.
The source code here on GitHub should be updated.

## Chapter 6
Changed some things in CategoryList and TagList for reloading lists
```
 Items = await _api.GetCategoriesAsync();
```

## Chapter 7
Fixe the Weblient with a / on the DeleteTag and DeleteCategory

## Chapter 11
Add Server pre-rendering and cache, this is a great moment to talk about meta tags for WASM


## Chapter 12
Running .NET7 p6 I wasn't able to debug the WASM project.  
Error: No output has been recevied from the application  
Searching the internet whit was a problem in .NET7 p5 and might not have made it into p6.
For now I am going to assume this is a preview problem and revisit this later on.

## Chapter 13
Revisit to see if we need to update the project to .NET7 or not
