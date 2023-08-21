How to Debug
# Steps
## 1. Open ServiceGenerator.cs
## 2. Uncomment following section
```
//#if DEBUG
//        if (!Debugger.IsAttached)
//        {
//            Debugger.Launch();
//        }
//#endif 
```
## 3. Change some text one of the files
	src\Restarted.Api\Todo.cs
	src\Restarted.Api\WeatherForecast.cs
## 4. Build Api project
	This will open debug window
	Select open new visual studio instance option
