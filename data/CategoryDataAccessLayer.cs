using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIMPLEAPI_Instructor.models;

namespace SIMPLEAPI_Instructor.data
{
    public class CategoryDataAccessLayer : ICategory01
    {
        private List<Category01> _categories = new List<Category01>();

        public CategoryDataAccessLayer()
        {
            _categories.Add(new Category01 {CategoryId = 1, CategoryName = "Gatot Subroto Ambatukam"});
            _categories.Add(new Category01 {CategoryId = 2, CategoryName = "Gatot Subroto Ambatukam"});
            _categories.Add(new Category01 {CategoryId = 3, CategoryName = "Gatot Subroto Ambatukam"});
            _categories.Add(new Category01 {CategoryId = 4, CategoryName = "Gatot Subroto Ambatukam"});
            _categories.Add(new Category01 {CategoryId = 5, CategoryName = "Gatot Subroto Ambatukam"});
        }
        public Category01 AddCategory(Category01 category)
        {
            _categories.Add(category);
            return category;
        }

        public void DeleteCategory(int id)
        {
            var category = GetCategory(id);
            _categories.Remove(category);
        }

        public List<Category01> GetCategories()
        {
            return _categories;
        }

        public Category01 GetCategory(int id)
        {
            var category = _categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                throw new Exception("Category not found");
            }
            return category;
        }

        public Category01 UpdateCategory(Category01 category)
        {
            var categoryToUpdate = GetCategory(category.CategoryId);
            categoryToUpdate.CategoryName = category.CategoryName;
            return category;
        }
    }
}