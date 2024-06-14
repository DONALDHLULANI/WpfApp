using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeApp
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes = new List<Recipe>();
        private Recipe currentRecipe;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            currentRecipe = new Recipe { Name = RecipeNameTextBox.Text };
            recipes.Add(currentRecipe);
            UpdateRecipeList();
            DisplayRecipe(currentRecipe);
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentRecipe != null)
            {

                double scaleFactor = 1.0;
                if (ScaleComboBox.SelectedItem != null)
                {
                    scaleFactor = double.Parse((ScaleComboBox.SelectedItem as ComboBoxItem).Content.ToString());
                }

                double quantity = double.Parse(IngredientQuantityTextBox.Text) * scaleFactor;

                var newIngredient = new Ingredient
                {
                    Name = IngredientNameTextBox.Text,
                    Quantity = quantity,
                    OriginalQuantity = quantity / scaleFactor,
                    Unit = IngredientUnitTextBox.Text,
                    Calories = int.Parse(IngredientCaloriesTextBox.Text),
                    FoodGroup = IngredientFoodGroupTextBox.Text
                };

                currentRecipe.Ingredients.Add(newIngredient);

                DisplayRecipe(currentRecipe);
            }
        }

        private void AddStepButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentRecipe != null)
            {
                currentRecipe.Steps.Add(StepDescriptionTextBox.Text);
                DisplayRecipe(currentRecipe);
            }
        }

        private void ClearAllButton_Click(object sender, RoutedEventArgs e)
        {
            RecipeNameTextBox.Clear();
            IngredientNameTextBox.Clear();
            IngredientQuantityTextBox.Clear();
            IngredientUnitTextBox.Clear();
            IngredientCaloriesTextBox.Clear();
            StepDescriptionTextBox.Clear();
            ScaleComboBox.SelectedItem = null;
            IngredientFoodGroupTextBox.Clear();
            currentRecipe = null;
            RecipeDisplayTextBlock.Text = string.Empty;

            recipes.Clear();
            UpdateRecipeList();
        }

        private void RecipeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecipeListBox.SelectedItem != null)
            {
                string selectedRecipeName = RecipeListBox.SelectedItem.ToString();
                Recipe selectedRecipe = recipes.FirstOrDefault(r => r.Name == selectedRecipeName);
                if (selectedRecipe != null)
                {
                    currentRecipe = selectedRecipe;
                    DisplayRecipe(selectedRecipe);
                }
            }
        }

        private void DisplayRecipe(Recipe recipe)
        {
            RecipeDisplayTextBlock.Text = $"Name: {recipe.Name}\n\nIngredients:\n";
            foreach (var ingredient in recipe.Ingredients)
            {
                RecipeDisplayTextBlock.Text += $"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name} ({ingredient.FoodGroup}, {ingredient.Calories})\n";
            }
            RecipeDisplayTextBlock.Text += $"\nSteps:\n";
            for (int i = 0; i < recipe.Steps.Count; i++)
            {
                RecipeDisplayTextBlock.Text += $"{i + 1}. {recipe.Steps[i]}\n";
            }

            double totalCalories = recipe.Ingredients.Sum(ing => ing.Calories);
            RecipeDisplayTextBlock.Text += $"\nTotal Calories: {totalCalories}";

            if (totalCalories > 300)
            {
                MessageBox.Show($"Warning: The total calories of the recipe exceed 300 (Total: {totalCalories})", "Calorie Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void UpdateRecipeList()
        {
            RecipeListBox.ItemsSource = null;
            RecipeListBox.ItemsSource = recipes.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            List<Recipe> filteredRecipes = recipes.Where(r =>
            {
                if (!string.IsNullOrWhiteSpace(FilterIngredientTextBox.Text) &&
                    !r.Ingredients.Any(ing => ing.Name.ToLower().Contains(FilterIngredientTextBox.Text.ToLower())))
                {
                    return false;
                }

                if (!string.IsNullOrWhiteSpace(FilterFoodGroupTextBox.Text) &&
                    !r.Ingredients.Any(ing => ing.FoodGroup.ToLower().Contains(FilterFoodGroupTextBox.Text.ToLower())))
                {
                    return false;
                }

                if (!string.IsNullOrWhiteSpace(MaxCaloriesTextBox.Text) &&
                    r.Ingredients.Sum(ing => ing.Calories) > double.Parse(MaxCaloriesTextBox.Text))
                {
                    return false;
                }

                return true;
            }).ToList();

            RecipeListBox.ItemsSource = null;
            RecipeListBox.ItemsSource = filteredRecipes.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        private void ResetFilterButton_Click(object sender, RoutedEventArgs e)
        {
            FilterIngredientTextBox.Clear();
            FilterFoodGroupTextBox.Clear();
            MaxCaloriesTextBox.Clear();
            RecipeListBox.ItemsSource = null;
            RecipeListBox.ItemsSource = recipes.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        private void DeleteRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentRecipe != null)
            {
                recipes.Remove(currentRecipe);
                currentRecipe = null;
                UpdateRecipeList();
                RecipeDisplayTextBlock.Text = string.Empty;
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<string> Steps { get; set; } = new List<string>();
        public double Scale { get; set; } = 1;
    }

    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double OriginalQuantity { get; set; }
        public string Unit { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }
    }
}
