using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIMPLEAPI_Instructor.models;

namespace SIMPLEAPI_Instructor.data
{
    public interface ICategory01
    {
        public List<Category01> GetCategories();
        public Category01 GetCategory(int id);
        public Category01 AddCategory(Category01 category);
        public Category01 UpdateCategory(Category01 category);
        public void DeleteCategory(int id);
        
    }
}