# JsonDoc
A simple way to create commented JSON documents for configuration purposes.

My intention was to create a tool for commenting json based configuration files. 
Json does not allow any commenting as a standard, but most of the serializers can work with some comments in the document. 
For configuration files it is essential to have some explanatory documentation over the parameters especially 
if the parameter set is large enough.I am using this technique for some time now, and I am very satisfied with the results. 
You can generate your default config file with an ease, and well commented for the operators.

### Usage:
\[JsonDoc("This is a class level doc A")\]
public class BaseClassA
{
    \[JsonDoc("This is a string property")\]
    public string STRING { get; set; }
}
