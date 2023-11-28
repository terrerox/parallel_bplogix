# Containers/Collections Usage

## Lists
- Lists in C# are part of the `System.Collections.Generic` namespace and provide an ordered, index-based collection.
- Elements in a list are accessed by their index, starting from 0.
- Use lists when you need a dynamic, ordered collection of elements.

Example:
```csharp
List<int> myList = new List<int> { 1, 2, 3, 4, 5 };
```

## Dictionaries
- Dictionaries in C# are part of the `System.Collections.Generic` namespace and represent a collection of key-value pairs.
- Elements in a dictionary are accessed by their keys, and each key must be unique.
- Use dictionaries when you need to associate values with specific keys for efficient lookup.

Example:
```csharp
Dictionary<string, int> myDict = new Dictionary<string, int>
{
    {"One", 1},
    {"Two", 2},
    {"Three", 3}
};
```

## Differences and Use Cases
- Use dictionaries when you need a collection with fast key-based access. 
- Dictionaries are suitable when you have a set of unique keys and you need to quickly look up values associated with those keys.

- Use lists when you need an ordered collection that you want to access by index.
- Lists are suitable when maintaining a sequence of items, and the order of the elements is important.
