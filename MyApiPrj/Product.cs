using System.ComponentModel.Design;
//this is "many" side
public class Product
{
    public int ProductIdentifier { get; set; }
    public  string ProductName { get; set; }
    public int ProductQuantityInStock { get; set; }
    //This is the foreign key property.
    //  It holds the value of the ManufacturerIdentifier from the Manufacture that this specific Product belongs to.
//    This is the critical piece of data that creates the link in the database.
    public int ManufactureTraceId { get; set; }

    //This is a reference navigation property.
    //It is a reference to the single Manufacture object that this Product belongs to. 
    //This represents the "One" part of the relationship from the child's perspective.
    public virtual Manufacture ProductManufacture { get; set; }


}

//FOREIGN KEY
// +--------------+-------------+------+-----+
// | DepartmentId | Name        | ...  |     |  ← "One" side (Parent)
// +--------------+-------------+------+-----+
// | 1            | HR          | ...  |     |
// | 2            | IT          | ...  |     |
// +--------------+-------------+------+-----+

// +-------------+-------------+------+-----+
// | EmployeeId  | Name        | DeptId|     |  ← "Many" side (Child) - FOREIGN KEY HERE
// +-------------+-------------+------+-----+
// | 101         | John        | 1     |     |  ← John belongs to HR (DeptId 1)
// | 102         | Sarah       | 1     |     |  ← Sarah belongs to HR (DeptId 1)
// | 103         | Mike        | 2     |     |  ← Mike belongs to IT (DeptId 2)
// +-------------+-------------+------+-----+


// +--------------+-------------+-------------+-----+
// | DepartmentId | Name        | EmployeeId  |     |  ← WRONG: Foreign key on "one" side
// +--------------+-------------+-------------+-----+
// | 1            | HR          | 101         |     |  ← Can only reference ONE employee
// | 2            | IT          | 103         |     |  ← Can only reference ONE employee
// +--------------+-------------+-------------+-----+

// +-------------+-------------+------+-----+
// | EmployeeId  | Name        | ...  |     |
// +-------------+-------------+------+-----+
// | 101         | John        | ...  |     |
// | 102         | Sarah       | ...  |     |  ← Sarah can't be linked to HR!
// | 103         | Mike        | ...  |     |
// +-------------+-------------+------+-----+


// NAVIGATION PROPERTIES: ARE THEY REQUIRED ON BOTH SIDES?

// SHORT ANSWER: No, they are NOT required on both sides.

// THREE POSSIBLE CONFIGURATIONS:

// 1. ONE-WAY RELATIONSHIP (Navigation on one side only)
//    - Example: Student → Classroom (but Classroom doesn't know about Students)
//    - Use when: You only need to navigate in one direction
//    - More lightweight, less overhead

// 2. TWO-WAY RELATIONSHIP (Navigation on both sides)
//    - Example: Course ↔ Teacher (both know about each other)
//    - Use when: You need to navigate in both directions
//    - More convenient, but slightly more overhead

// 3. NO NAVIGATION PROPERTIES (Just foreign keys)
//    - Example: Only IDs, no object references
//    - Use when: You want maximum performance and minimal overhead

// WHEN TO USE EACH APPROACH:

// ONE-WAY (Recommended for most cases):
// - When you only need to navigate from child to parent
// - When performance is critical
// - When you want to avoid circular references

// TWO-WAY (Use when needed):
// - When you need to navigate from parent to children
// - When you frequently need to access related data
// - When convenience is more important than performance

// EXAMPLE SCENARIOS:

// One-Way Relationships:
// - Student → Classroom (students know their classroom, but classroom doesn't track students)
// - Order → Customer (orders know their customer, but customer doesn't track all orders)

// Two-Way Relationships:
// - Author ↔ Books (authors know their books, books know their author)
// - Department ↔ Employees (department knows employees, employees know department)
// ";
//     }