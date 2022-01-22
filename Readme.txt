The solution contains two Program classes:
    a. Program.cs: It shows the simple use of service calling (Since the requirement was not any external libraries)
    b. Program1.cs: It shows the use of Dependency Injection (Uses Microsoft.Extensions.DependencyInjection)

Assumption 
1. To run the application 'Data' folder should contain 3 files
        a. Data1.csv
        b. Data2.csv
        c. Devices.csv

2. Data folder path is considered as 3 level up the executing assembly.

Recommendation
1. Data folder path can be saved in an config or settings file.