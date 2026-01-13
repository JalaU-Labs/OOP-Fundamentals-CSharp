using OOPFundamentals.Core.Abstraction;
using Xunit;

namespace OOPFundamentals.Tests;

/// <summary>
/// Unit tests for Abstraction pillar classes.
/// Tests abstract classes (Shape) and interfaces (IDrawable, IResizable).
/// </summary>
public class AbstractionTests
{
    #region Circle Tests
    
    [Fact]
    public void Circle_Constructor_ShouldInitializeCorrectly()
    {
        // Arrange & Act
        var circle = new Circle(5, "Red");
        
        // Assert
        Assert.Equal(5, circle.Radius);
        Assert.Equal("Red", circle.Color);
        Assert.Equal("Circle", circle.Name);
    }
    
    [Theory]
    [InlineData(5, 78.54)]
    [InlineData(10, 314.16)]
    [InlineData(3, 28.27)]
    public void Circle_CalculateArea_ShouldReturnCorrectArea(double radius, double expectedArea)
    {
        // Arrange
        var circle = new Circle(radius, "Red");
        
        // Act
        double area = circle.CalculateArea();
        
        // Assert
        Assert.Equal(expectedArea, area, 2); // 2 decimal places precision
    }
    
    [Theory]
    [InlineData(5, 31.42)]
    [InlineData(10, 62.83)]
    [InlineData(3, 18.85)]
    public void Circle_CalculatePerimeter_ShouldReturnCorrectPerimeter(double radius, double expectedPerimeter)
    {
        // Arrange
        var circle = new Circle(radius, "Red");
        
        // Act
        double perimeter = circle.CalculatePerimeter();
        
        // Assert
        Assert.Equal(expectedPerimeter, perimeter, 2);
    }
    
    [Fact]
    public void Circle_Diameter_ShouldReturnCorrectValue()
    {
        // Arrange
        var circle = new Circle(5, "Red");
        
        // Act
        double diameter = circle.Diameter;
        
        // Assert
        Assert.Equal(10, diameter);
    }
    
    [Fact]
    public void Circle_Resize_ShouldChangeRadius()
    {
        // Arrange
        var circle = new Circle(5, "Red");
        double originalArea = circle.CalculateArea();
        
        // Act
        circle.Resize(2.0); // Double the size
        
        // Assert
        Assert.Equal(10, circle.Radius);
        Assert.Equal(originalArea * 4, circle.CalculateArea(), 2); // Area scales by square
    }
    
    [Fact]
    public void Circle_ContainsPoint_ShouldReturnTrue_WhenPointInside()
    {
        // Arrange
        var circle = new Circle(5, "Red");
        
        // Act
        bool contains = circle.ContainsPoint(3, 0);
        
        // Assert
        Assert.True(contains);
    }
    
    [Fact]
    public void Circle_ContainsPoint_ShouldReturnFalse_WhenPointOutside()
    {
        // Arrange
        var circle = new Circle(5, "Red");
        
        // Act
        bool contains = circle.ContainsPoint(10, 0);
        
        // Assert
        Assert.False(contains);
    }
    
    #endregion
    
    #region Rectangle Tests
    
    [Fact]
    public void Rectangle_Constructor_ShouldInitializeCorrectly()
    {
        // Arrange & Act
        var rectangle = new Rectangle(4, 6, "Blue");
        
        // Assert
        Assert.Equal(4, rectangle.Width);
        Assert.Equal(6, rectangle.Height);
        Assert.Equal("Blue", rectangle.Color);
        Assert.Equal("Rectangle", rectangle.Name);
    }
    
    [Theory]
    [InlineData(4, 6, 24)]
    [InlineData(5, 5, 25)]
    [InlineData(10, 2, 20)]
    public void Rectangle_CalculateArea_ShouldReturnCorrectArea(double width, double height, double expectedArea)
    {
        // Arrange
        var rectangle = new Rectangle(width, height, "Blue");
        
        // Act
        double area = rectangle.CalculateArea();
        
        // Assert
        Assert.Equal(expectedArea, area);
    }
    
    [Theory]
    [InlineData(4, 6, 20)]
    [InlineData(5, 5, 20)]
    [InlineData(10, 2, 24)]
    public void Rectangle_CalculatePerimeter_ShouldReturnCorrectPerimeter(
        double width, double height, double expectedPerimeter)
    {
        // Arrange
        var rectangle = new Rectangle(width, height, "Blue");
        
        // Act
        double perimeter = rectangle.CalculatePerimeter();
        
        // Assert
        Assert.Equal(expectedPerimeter, perimeter);
    }
    
    [Fact]
    public void Rectangle_IsSquare_ShouldReturnTrue_WhenSidesEqual()
    {
        // Arrange
        var rectangle = new Rectangle(5, 5, "Blue");
        
        // Act
        bool isSquare = rectangle.IsSquare;
        
        // Assert
        Assert.True(isSquare);
        Assert.Equal("Square", rectangle.Name); // Name should change to Square
    }
    
    [Fact]
    public void Rectangle_IsSquare_ShouldReturnFalse_WhenSidesDifferent()
    {
        // Arrange
        var rectangle = new Rectangle(4, 6, "Blue");
        
        // Act
        bool isSquare = rectangle.IsSquare;
        
        // Assert
        Assert.False(isSquare);
    }
    
    [Fact]
    public void Rectangle_CreateSquare_ShouldCreateSquare()
    {
        // Arrange & Act
        var square = Rectangle.CreateSquare(5, "Green");
        
        // Assert
        Assert.Equal(5, square.Width);
        Assert.Equal(5, square.Height);
        Assert.True(square.IsSquare);
        Assert.Equal("Square", square.Name);
    }
    
    [Fact]
    public void Rectangle_Diagonal_ShouldCalculateCorrectly()
    {
        // Arrange
        var rectangle = new Rectangle(3, 4, "Blue");
        
        // Act
        double diagonal = rectangle.Diagonal;
        
        // Assert
        Assert.Equal(5, diagonal, 2); // 3-4-5 triangle
    }
    
    [Fact]
    public void Rectangle_Rotate90Degrees_ShouldSwapDimensions()
    {
        // Arrange
        var rectangle = new Rectangle(4, 6, "Blue");
        
        // Act
        rectangle.Rotate90Degrees();
        
        // Assert
        Assert.Equal(6, rectangle.Width);
        Assert.Equal(4, rectangle.Height);
    }
    
    #endregion
    
    #region Triangle Tests
    
    [Fact]
    public void Triangle_Constructor_ShouldInitializeCorrectly()
    {
        // Arrange & Act
        var triangle = new Triangle(3, 4, 5, "Green");
        
        // Assert
        Assert.Equal(3, triangle.SideA);
        Assert.Equal(4, triangle.SideB);
        Assert.Equal(5, triangle.SideC);
        Assert.Equal("Green", triangle.Color);
    }
    
    [Fact]
    public void Triangle_Constructor_ShouldThrowException_WhenInvalidSides()
    {
        // Arrange, Act & Assert
        // Triangle inequality: sum of any two sides must be greater than third side
        Assert.Throws<ArgumentException>(() => new Triangle(1, 2, 10, "Green"));
    }
    
    [Fact]
    public void Triangle_CalculateArea_ShouldUseHeronsFormula()
    {
        // Arrange
        var triangle = new Triangle(3, 4, 5, "Green"); // Right triangle
        
        // Act
        double area = triangle.CalculateArea();
        
        // Assert
        Assert.Equal(6, area, 2); // Area of 3-4-5 right triangle is 6
    }
    
    [Fact]
    public void Triangle_CalculatePerimeter_ShouldSumAllSides()
    {
        // Arrange
        var triangle = new Triangle(3, 4, 5, "Green");
        
        // Act
        double perimeter = triangle.CalculatePerimeter();
        
        // Assert
        Assert.Equal(12, perimeter);
    }
    
    [Fact]
    public void Triangle_Type_ShouldBeEquilateral_WhenAllSidesEqual()
    {
        // Arrange
        var triangle = new Triangle(5, 5, 5, "Green");
        
        // Act
        var type = triangle.Type;
        
        // Assert
        Assert.Equal(TriangleType.Equilateral, type);
    }
    
    [Fact]
    public void Triangle_Type_ShouldBeIsosceles_WhenTwoSidesEqual()
    {
        // Arrange
        var triangle = new Triangle(5, 5, 3, "Green");
        
        // Act
        var type = triangle.Type;
        
        // Assert
        Assert.Equal(TriangleType.Isosceles, type);
    }
    
    [Fact]
    public void Triangle_Type_ShouldBeScalene_WhenAllSidesDifferent()
    {
        // Arrange
        var triangle = new Triangle(3, 4, 5, "Green");
        
        // Act
        var type = triangle.Type;
        
        // Assert
        Assert.Equal(TriangleType.Scalene, type);
    }
    
    [Fact]
    public void Triangle_IsRightTriangle_ShouldReturnTrue_ForRightTriangle()
    {
        // Arrange
        var triangle = new Triangle(3, 4, 5, "Green");
        
        // Act
        bool isRight = triangle.IsRightTriangle;
        
        // Assert
        Assert.True(isRight);
    }
    
    [Fact]
    public void Triangle_CreateEquilateral_ShouldCreateEquilateralTriangle()
    {
        // Arrange & Act
        var triangle = Triangle.CreateEquilateral(5, "Green");
        
        // Assert
        Assert.Equal(5, triangle.SideA);
        Assert.Equal(5, triangle.SideB);
        Assert.Equal(5, triangle.SideC);
        Assert.Equal(TriangleType.Equilateral, triangle.Type);
    }
    
    [Fact]
    public void Triangle_CreateIsosceles_ShouldCreateIsoscelesTriangle()
    {
        // Arrange & Act
        var triangle = Triangle.CreateIsosceles(5, 3, "Green");
        
        // Assert
        Assert.Equal(5, triangle.SideA);
        Assert.Equal(5, triangle.SideB);
        Assert.Equal(3, triangle.SideC);
        Assert.Equal(TriangleType.Isosceles, triangle.Type);
    }
    
    #endregion
    
    #region Shape Comparison Tests
    
    [Fact]
    public void Shape_CompareAreaWith_ShouldReturnCorrectComparison()
    {
        // Arrange
        var circle = new Circle(5, "Red");
        var rectangle = new Rectangle(10, 10, "Blue");
        
        // Act
        string comparison = circle.CompareAreaWith(rectangle);
        
        // Assert
        Assert.Contains("smaller", comparison, StringComparison.OrdinalIgnoreCase);
    }
    
    [Fact]
    public void Shape_IsLargerThan_ShouldReturnTrue_WhenAreaIsLarger()
    {
        // Arrange
        var largeCircle = new Circle(10, "Red");
        var smallCircle = new Circle(5, "Blue");
        
        // Act
        bool isLarger = largeCircle.IsLargerThan(smallCircle);
        
        // Assert
        Assert.True(isLarger);
    }
    
    [Fact]
    public void Shape_IsLargerThan_ShouldReturnFalse_WhenAreaIsSmaller()
    {
        // Arrange
        var smallCircle = new Circle(5, "Red");
        var largeCircle = new Circle(10, "Blue");
        
        // Act
        bool isLarger = smallCircle.IsLargerThan(largeCircle);
        
        // Assert
        Assert.False(isLarger);
    }
    
    #endregion
    
    #region DrawableCircle Tests
    
    [Fact]
    public void DrawableCircle_ShouldImplementIDrawable()
    {
        // Arrange & Act
        var drawableCircle = new DrawableCircle(5, "Purple");
        
        // Assert
        Assert.IsAssignableFrom<IDrawable>(drawableCircle);
    }
    
    [Fact]
    public void DrawableCircle_ShouldImplementIResizable()
    {
        // Arrange & Act
        var drawableCircle = new DrawableCircle(5, "Purple");
        
        // Assert
        Assert.IsAssignableFrom<IResizable>(drawableCircle);
    }
    
    [Fact]
    public void DrawableCircle_ResizeToFit_ShouldAdjustRadius()
    {
        // Arrange
        var drawableCircle = new DrawableCircle(10, "Purple");
        
        // Act
        drawableCircle.ResizeToFit(10, 10);
        
        // Assert
        Assert.True(drawableCircle.Radius <= 5); // Should fit in 10x10 box
    }
    
    [Fact]
    public void DrawableCircle_ShouldInheritFromCircle()
    {
        // Arrange & Act
        var drawableCircle = new DrawableCircle(5, "Purple");
        
        // Assert
        Assert.IsAssignableFrom<Circle>(drawableCircle);
        Assert.IsAssignableFrom<Shape>(drawableCircle);
    }
    
    #endregion
    
    #region Interface Polymorphism Tests
    
    [Fact]
    public void IDrawable_CanBeUsedPolymorphically()
    {
        // Arrange
        IDrawable drawable = new DrawableCircle(5, "Purple");
        
        // Act & Assert
        Assert.Equal("Purple", drawable.Color);
        // Draw method should not throw
        drawable.Draw();
    }
    
    [Fact]
    public void IResizable_CanBeUsedPolymorphically()
    {
        // Arrange
        IResizable resizable = new DrawableCircle(5, "Purple");
        
        // Act
        resizable.Resize(2.0);
        
        // Assert - no exception means success
        Assert.True(true);
    }
    
    #endregion
}