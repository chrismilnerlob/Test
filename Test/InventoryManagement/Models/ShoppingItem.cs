namespace IcelandTest.InventoryManagement.Models
{
    // Use of inheritence - normally I'd keep these 3 fields together, this is just for demonstration
    // I tend to lean towards implementing interfaces with DI, focusing more on Encapsulation rather than 
    // Inheritence. If I do inherit from a class, it's usually an abstract class with default methods

    public class ShoppingItem : IMCustomDataObject 
    {
        public string Name { get; set; }
    }
}
