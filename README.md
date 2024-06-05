
# Rhino Command Test

This project was created to participate in the [XFrame RhinoCommon-Coding-Challenge-2024](https://github.com/X-Frame/RhinoCommon-Coding-Challenge-2024).

Two primary goals:
1. Demonstrate basic programming skills and the ability to complete related development tasks;
2. Accumulate domain knowledge to facilitate in-depth and engaging communication  

Therefore, we plan to achieve these two goals by completing these tasks [Still under continuous update] :  
| Goal id | task state | plan or finish date | version | task introduction |
| :--:| :---: | :---:| :---: | :--------- |
| 1, 2 | Finish | 04-06-2024 | P0.0,1 | implement a minimum viable product (MVP) |
| 1, 2 | Finish | 04-06-2024 | P0.0.1 | Set up the environment  |
| 1, 2 | TODO   | 05-06-2024 | P0.0.2 | Refactor code and modularize code |


# Version  
| version | date | State | introduction |
| :--:| :---: | :---: | :----- |
| P0.0.3 | 05-06-2024 | TODO   | ...... |
| [P0.0.2](#p0-0-2) | 05-06-2024 | TODO   | Refactor the code and organize it in a way that is familiar to me |
| [P0.0.1](#p0-0-1) | 04-06-2024 | Finish | Initialize the Mac development environment. Implement a simplified version |

## P0.0.2
1. Independent configuration file
2. Independent calculation implementation
3. Optional: Include unit tests

## P0.0.1  
1. Set up the environment
2. implement a minimum viable product (MVP)


# Spend Time  
| id | version |task | date | hours | sum hours |
| :--:| :--: | :------ | :---:| :---: | :---: |
| 1 | P0.0.1 | read require | 04-06-2024 | 0.2 | 0.2 |
| 2 | P0.0.1 | init environment | 04-06-2024 | 1 | 1.2  |
| 3 | P0.0.1 | first mini MVP | 04-06-2024 | 0.3 | 1.5  |
| 4 | P0.0.1 | read require again and init readme   | 04-06-2024 | 0.8 | 2.3 |
| 5 | P0.0.2 | Refactor the code to separate the calculation logic into Agent and BoxManager. | 05-06-2024 | 1 | 3.3  |
| 6 | P0.0.2 | use InstanceObecjt to impl box. | 05-06-2024 | 0.2 | 3.5  |

supplementary statement :
1. **id:2** Initially, I tried to manually create the plugin through ChatGPT, but it didn't work. I then turned to Google to check the official documentation and set up the basic environment by installing VS plugins.
2. **id:3** It took me about 20 minutes to refactor the code. I wanted to use a separate namespace, but I'm not familiar with Visual Studio and C#'s namespace mechanism. After spending 30 minutes without successfully referencing it, I'll set aside namespaces for now and ensure it runs correctly first.


# FAQ  
These are just my opinions and not the only options. In some cases, I don't have a strong preference; this is just my usual way of handling things.

## Q1 About Agent and XXManager
most of the business logic can be separated from the UI. The business logic can be accessed through a single Agent, which is designed as a singleton. Different business processes will be accessed through the Agent.
Structurally, it looks like this :  
```
--HelloRhiCommon
----Agent
------BoxManager
------Box
------...
------XXManager
------XX
```
Access and call using the following method :
```
var boxManager = Agent.Instance.BoxManager;
boxManager.CreateBox(doc);
```
I usually write it this way when developing an SDK.

## Q2 parameter vlidation and Excepition
I don't fully understand the edge cases in the logic (such as a parameter that makes it impossible to create a BOX). I will focus on explaining parameter validation, which mainly includes two situations:
1. validation of user input parameters on the interface
2. validation of parameters in function methods

From a programming perspective, it includes:
1. choosing the appropriate design patterns for implementation (to facilitate maintenance and modification);
2. deciding when to use return values and when to use exceptions.

In actual programming, it's not always necessary to strictly follow the best practices recommended for a specific language. Sometimes, simply using different return values is sufficient to achieve the functionality. Therefore, if this part is designed collaboratively, it's best to agree on parameter validation and exception handling conventions from the beginning.

## Q3 parameter vlidation and annotations(AOP)
Among the engineers I've worked with before, some are very fond of using annotations. I don't have a strong preference, but sometimes they can make the code appear more sophisticated.
For example, in BoxManager, several methods need to check if the passed doc object is null.
Therefore, a static method can be created independently.   
```
public static void ValidateDoc(RhinoDoc doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException(nameof(doc), "The Rhino document cannot be null.");
            }
        }
```
called wherever needed ： 
```
public int CreateBox(RhinoDoc doc)
{
    BoxManager.ValidateDoc(doc);
    ... ...

```

If using method annotations, frameworks like AOP in .NET (such as PostSharp or Fody) can be utilized to implement method-level annotations.
TODO



# Resources:
* [rhino Doc : init mac environment](https://developer.rhino3d.com/guides/rhinocommon/your-first-plugin-mac/)
* [rhino Doc : rhinocommon](https://developer.rhino3d.com/samples/#rhinocommon)
