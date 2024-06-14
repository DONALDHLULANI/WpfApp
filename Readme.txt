Recipe App
**********

Overview
********

Recipe App is a WPF application designed to help users manage recipes by adding, viewing, filtering, and deleting recipes and their respective ingredients and steps. The application also warns users when the total calories of a recipe exceed a specified limit.

Features
********

- Add new recipes
- Add ingredients to recipes with scaling functionality
- Add steps to recipes
- View recipe details
- Filter recipes by ingredient, food group, and maximum calories
- Delete selected recipes
- Clear all input fields
- Exit the application

Requirements
************

- Visual Studio 2019 or later
- .NET 6.0 SDK or later

Installation and Setup
**********************

1. Clone the Repository
-----------------------

   Clone the repository to your local machine using the following command:

   git clone <repository-url>

   Replace `<repository-url>` with the URL of your repository.

2. Open the Project
-------------------

   Open the solution file `RecipeApp.sln` in Visual Studio.

3. Restore NuGet Packages
-------------------------

   In Visual Studio, go to `Tools` > `NuGet Package Manager` > `Package Manager Console` and run the following command to restore the required NuGet packages:

   dotnet restore

4. Build the Project
--------------------

   Build the project by selecting `Build` > `Build Solution` from the menu or by pressing `Ctrl+Shift+B`.

Running the Application
***********************

1. Start the Application
------------------------

   Start the application by pressing `F5` or selecting `Debug` > `Start Debugging` from the menu.

2. Using the Application
------------------------

   - Add Recipe: Enter the recipe name and click `Add Recipe`.
   - Add Ingredient: Enter the ingredient details (name, quantity, unit, calories, food group), select the scale factor from the dropdown, and click `Add Ingredient`.
   - Add Step: Enter the step description and click `Add Step`.
   - View Recipe: Select a recipe from the list on the left to view its details.
   - Filter Recipes: Enter filter criteria (ingredient, food group, max calories) and click `Apply Filter`.
   - Reset Filter: Click `Reset Filter` to clear the filter criteria and display all recipes.
   - Delete Recipe: Click `Delete Recipe` to remove the selected recipe.
   - Clear All: Click `Clear All` to clear all input fields and reset the application state.
   - Exit: Click `Exit` to close the application.

Additional Information
**********************

- Recipe Scaling: When adding an ingredient, the quantity is scaled according to the selected factor. The original quantity is stored for potential future use or resetting.
- Calorie Warning: If the total calories of a recipe exceed 300, a warning message is displayed.

---

This README provides the necessary steps to compile and run the Recipe App. Follow these instructions to get started with the application and explore its features.