using OOPFundamentals.Core.Inheritance;
using Xunit;

namespace OOPFundamentals.Tests;

/// <summary>
/// Unit tests for Inheritance pillar classes.
/// Tests the vehicle hierarchy: Vehicle, Car, Motorcycle, Truck, and ElectricCar.
/// </summary>
public class InheritanceTests
{
    #region Vehicle Base Class Tests
    
    [Fact]
    public void Vehicle_Constructor_ShouldInitializeBaseProperties()
    {
        // Arrange & Act
        var car = new Car("Toyota", "Camry", 2024, 4);
        
        // Assert
        Assert.Equal("Toyota", car.Brand);
        Assert.Equal("Camry", car.Model);
        Assert.Equal(2024, car.Year);
        Assert.False(car.IsEngineRunning);
        Assert.Equal(0, car.CurrentSpeed);
    }
    
    [Fact]
    public void Vehicle_Start_ShouldStartEngine()
    {
        // Arrange
        var car = new Car("Toyota", "Camry", 2024, 4);
        
        // Act
        car.Start();
        
        // Assert
        Assert.True(car.IsEngineRunning);
    }
    
    [Fact]
    public void Vehicle_Stop_ShouldStopEngine_WhenNotMoving()
    {
        // Arrange
        var car = new Car("Toyota", "Camry", 2024, 4);
        car.Start();
        
        // Act
        car.Stop();
        
        // Assert
        Assert.False(car.IsEngineRunning);
    }
    
    [Fact]
    public void Vehicle_Accelerate_ShouldIncreaseSpeed()
    {
        // Arrange
        var car = new Car("Toyota", "Camry", 2024, 4);
        car.Start();
        
        // Act
        car.Accelerate(50);
        
        // Assert
        Assert.Equal(50, car.CurrentSpeed);
    }
    
    [Fact]
    public void Vehicle_Accelerate_ShouldNotExceedMaxSpeed()
    {
        // Arrange
        var car = new Car("Toyota", "Camry", 2024, 4);
        car.Start();
        
        // Act
        car.Accelerate(200); // Exceeds max speed
        
        // Assert
        Assert.Equal(car.MaxSpeed, car.CurrentSpeed);
    }
    
    [Fact]
    public void Vehicle_Brake_ShouldDecreaseSpeed()
    {
        // Arrange
        var car = new Car("Toyota", "Camry", 2024, 4);
        car.Start();
        car.Accelerate(60);
        
        // Act
        car.Brake(20);
        
        // Assert
        Assert.Equal(40, car.CurrentSpeed);
    }
    
    [Fact]
    public void Vehicle_Brake_ShouldNotGoBelowZero()
    {
        // Arrange
        var car = new Car("Toyota", "Camry", 2024, 4);
        car.Start();
        car.Accelerate(30);
        
        // Act
        car.Brake(50);
        
        // Assert
        Assert.Equal(0, car.CurrentSpeed);
    }
    
    #endregion
    
    #region Car Tests
    
    [Fact]
    public void Car_Constructor_ShouldInitializeCarProperties()
    {
        // Arrange & Act
        var car = new Car("Toyota", "Camry", 2024, 4, TransmissionType.Automatic, true, "Silver");
        
        // Assert
        Assert.Equal(4, car.NumberOfDoors);
        Assert.Equal(TransmissionType.Automatic, car.Transmission);
        Assert.True(car.HasSunroof);
        Assert.Equal("Silver", car.Color);
    }
    
    [Fact]
    public void Car_OpenTrunk_ShouldWork_WhenNotMoving()
    {
        // Arrange
        var car = new Car("Toyota", "Camry", 2024, 4);
        
        // Act & Assert - should not throw
        car.OpenTrunk();
    }
    
    [Fact]
    public void Car_TurnOnAirConditioning_ShouldWork_WhenEngineRunning()
    {
        // Arrange
        var car = new Car("Toyota", "Camry", 2024, 4);
        car.Start();
        
        // Act
        car.TurnOnAirConditioning();
        
        // Assert
        Assert.True(car.IsAirConditioningOn);
    }
    
    [Fact]
    public void Car_Park_ShouldWork_WhenNotMoving()
    {
        // Arrange
        var car = new Car("Toyota", "Camry", 2024, 4);
        
        // Act & Assert - should not throw
        car.Park();
    }
    
    [Fact]
    public void Car_VehicleType_ShouldReturnCar()
    {
        // Arrange
        var car = new Car("Toyota", "Camry", 2024, 4);
        
        // Act
        string type = car.VehicleType;
        
        // Assert
        Assert.Equal("Car", type);
    }
    
    #endregion
    
    #region Motorcycle Tests
    
    [Fact]
    public void Motorcycle_Constructor_ShouldInitializeMotorcycleProperties()
    {
        // Arrange & Act
        var motorcycle = new Motorcycle("Harley-Davidson", "Street 750", 2024, 
                                       MotorcycleType.Cruiser, 750, "Black");
        
        // Assert
        Assert.Equal(MotorcycleType.Cruiser, motorcycle.Type);
        Assert.Equal(750, motorcycle.EngineCC);
        Assert.Equal(2, motorcycle.NumberOfWheels);
    }
    
    [Fact]
    public void Motorcycle_NumberOfWheels_ShouldBe3_WithSidecar()
    {
        // Arrange
        var motorcycle = new Motorcycle("Harley-Davidson", "Street 750", 2024, 
                                       MotorcycleType.Cruiser, 750, "Black");
        
        // Act
        motorcycle.ToggleSidecar(true);
        
        // Assert
        Assert.Equal(3, motorcycle.NumberOfWheels);
        Assert.True(motorcycle.HasSidecar);
    }
    
    [Fact]
    public void Motorcycle_MaxSpeed_ShouldBeHigher_ForSportBikes()
    {
        // Arrange
        var sportBike = new Motorcycle("Yamaha", "YZF-R1", 2024, 
                                      MotorcycleType.Sport, 1000, "Red");
        var cruiser = new Motorcycle("Harley", "Street", 2024, 
                                    MotorcycleType.Cruiser, 750, "Black");
        
        // Act & Assert
        Assert.True(sportBike.MaxSpeed > cruiser.MaxSpeed);
    }
    
    [Fact]
    public void Motorcycle_PutOnHelmet_ShouldSetHelmetStatus()
    {
        // Arrange
        var motorcycle = new Motorcycle("Yamaha", "YZF-R3", 2024, 
                                       MotorcycleType.Sport, 321, "Blue");
        
        // Act
        motorcycle.PutOnHelmet();
        
        // Assert
        Assert.True(motorcycle.IsWearingHelmet);
    }
    
    [Fact]
    public void Motorcycle_Accelerate_ShouldBeFaster_ThanCar()
    {
        // Arrange
        var motorcycle = new Motorcycle("Yamaha", "YZF-R3", 2024, 
                                       MotorcycleType.Sport, 321, "Blue");
        var car = new Car("Toyota", "Camry", 2024, 4);
        
        motorcycle.PutOnHelmet();
        motorcycle.RetractKickstand();
        motorcycle.Start();
        car.Start();
        
        // Act
        motorcycle.Accelerate(30); // Motorcycles have 1.5x boost
        car.Accelerate(30);
        
        // Assert
        Assert.True(motorcycle.CurrentSpeed > car.CurrentSpeed);
    }
    
    #endregion
    
    #region Truck Tests
    
    [Fact]
    public void Truck_Constructor_ShouldInitializeTruckProperties()
    {
        // Arrange & Act
        var truck = new Truck("Ford", "F-150", 2024, TruckType.Light, 1000, "White");
        
        // Assert
        Assert.Equal(TruckType.Light, truck.Type);
        Assert.Equal(1000, truck.CargoCapacity);
        Assert.Equal(0, truck.CurrentCargoWeight);
    }
    
    [Fact]
    public void Truck_LoadCargo_ShouldIncreaseCurrentWeight()
    {
        // Arrange
        var truck = new Truck("Ford", "F-150", 2024, TruckType.Light, 1000, "White");
        truck.OpenCargoBed();
        
        // Act
        truck.LoadCargo(500);
        
        // Assert
        Assert.Equal(500, truck.CurrentCargoWeight);
        Assert.Equal(50, truck.LoadPercentage);
    }
    
    [Fact]
    public void Truck_LoadCargo_ShouldNotExceedCapacity()
    {
        // Arrange
        var truck = new Truck("Ford", "F-150", 2024, TruckType.Light, 1000, "White");
        truck.OpenCargoBed();
        truck.LoadCargo(800);
        
        // Act
        truck.LoadCargo(300); // Would exceed capacity
        
        // Assert
        Assert.Equal(800, truck.CurrentCargoWeight); // Should remain unchanged
    }
    
    [Fact]
    public void Truck_UnloadCargo_ShouldDecreaseCurrentWeight()
    {
        // Arrange
        var truck = new Truck("Ford", "F-150", 2024, TruckType.Light, 1000, "White");
        truck.OpenCargoBed();
        truck.LoadCargo(500);
        
        // Act
        truck.UnloadCargo(200);
        
        // Assert
        Assert.Equal(300, truck.CurrentCargoWeight);
    }
    
    [Fact]
    public void Truck_MaxSpeed_ShouldDecrease_WithLoad()
    {
        // Arrange
        var truck = new Truck("Ford", "F-150", 2024, TruckType.Light, 1000, "White");
        double emptyMaxSpeed = truck.MaxSpeed;
        
        // Act
        truck.OpenCargoBed();
        truck.LoadCargo(800); // 80% capacity
        double loadedMaxSpeed = truck.MaxSpeed;
        
        // Assert
        Assert.True(loadedMaxSpeed < emptyMaxSpeed);
    }
    
    [Fact]
    public void Truck_AttachTrailer_ShouldIncreaseNumberOfWheels()
    {
        // Arrange
        var truck = new Truck("Ford", "F-150", 2024, TruckType.Medium, 2000, "White");
        int wheelsWithoutTrailer = truck.NumberOfWheels;
        
        // Act
        truck.AttachTrailer();
        
        // Assert
        Assert.True(truck.HasTrailer);
        Assert.Equal(wheelsWithoutTrailer + 4, truck.NumberOfWheels);
    }
    
    #endregion
    
    #region ElectricCar (Multi-Level Inheritance) Tests
    
    [Fact]
    public void ElectricCar_Constructor_ShouldInitializeElectricProperties()
    {
        // Arrange & Act
        var tesla = new ElectricCar("Tesla", "Model 3", 2024, 4, 75, 500, "Red");
        
        // Assert
        Assert.Equal(75, tesla.BatteryCapacity);
        Assert.Equal(500, tesla.ElectricRange);
        Assert.Equal(100, tesla.BatteryChargePercentage);
        Assert.Equal("Electric", tesla.FuelType);
    }
    
    [Fact]
    public void ElectricCar_ShouldInheritFromCar()
    {
        // Arrange & Act
        var tesla = new ElectricCar("Tesla", "Model 3", 2024, 4, 75, 500, "Red");
        
        // Assert
        Assert.IsAssignableFrom<Car>(tesla);
        Assert.IsAssignableFrom<Vehicle>(tesla);
    }
    
    [Fact]
    public void ElectricCar_StartCharging_ShouldSetChargingStatus()
    {
        // Arrange
        var tesla = new ElectricCar("Tesla", "Model 3", 2024, 4, 75, 500, "Red");
        tesla.RechargeBattery(-50); // Discharge to 50%
        
        // Act
        tesla.StartCharging(ChargingSpeed.Supercharger);
        
        // Assert
        Assert.True(tesla.IsCharging);
    }
    
    [Fact]
    public void ElectricCar_RechargeBattery_ShouldIncreaseBatteryLevel()
    {
        // Arrange
        var tesla = new ElectricCar("Tesla", "Model 3", 2024, 4, 75, 500, "Red");
        tesla.RechargeBattery(-30); // Discharge to 70%
        
        // Act
        tesla.RechargeBattery(20);
        
        // Assert
        Assert.Equal(90, tesla.BatteryChargePercentage, 1);
    }
    
    [Fact]
    public void ElectricCar_RemainingRange_ShouldCalculateCorrectly()
    {
        // Arrange
        var tesla = new ElectricCar("Tesla", "Model 3", 2024, 4, 75, 500, "Red");
        
        // Act
        double rangeAt100 = tesla.RemainingRange;
        tesla.RechargeBattery(-50); // 50% battery
        double rangeAt50 = tesla.RemainingRange;
        
        // Assert
        Assert.Equal(500, rangeAt100, 1);
        Assert.Equal(250, rangeAt50, 1);
    }
    
    [Fact]
    public void ElectricCar_SetRegenerativeBraking_ShouldChangeStatus()
    {
        // Arrange
        var tesla = new ElectricCar("Tesla", "Model 3", 2024, 4, 75, 500, "Red");
        
        // Act
        tesla.SetRegenerativeBraking(false);
        
        // Assert
        Assert.False(tesla.IsRegenerativeBrakingEnabled);
    }
    
    #endregion
    
    #region Polymorphic Behavior Tests
    
    [Fact]
    public void Vehicle_Polymorphism_AllVehicleTypesShouldWork()
    {
        // Arrange - Create different vehicle types
        List<Vehicle> fleet = new List<Vehicle>
        {
            new Car("Toyota", "Camry", 2024, 4),
            new Motorcycle("Harley", "Street", 2024, MotorcycleType.Cruiser, 750),
            new Truck("Ford", "F-150", 2024, TruckType.Light, 1000),
            new ElectricCar("Tesla", "Model 3", 2024, 4, 75, 500)
        };
        
        // Act - Start all vehicles polymorphically
        foreach (var vehicle in fleet)
        {
            if (vehicle is Motorcycle moto)
            {
                moto.PutOnHelmet();
                moto.RetractKickstand();
            }
            vehicle.Start();
        }
        
        // Assert - All should be running
        Assert.All(fleet, v => Assert.True(v.IsEngineRunning));
    }
    
    [Fact]
    public void Vehicle_GetInfo_ShouldBeOverriddenInDerivedClasses()
    {
        // Arrange
        Vehicle car = new Car("Toyota", "Camry", 2024, 4);
        Vehicle motorcycle = new Motorcycle("Harley", "Street", 2024, 
                                           MotorcycleType.Cruiser, 750);
        
        // Act
        string carInfo = car.GetInfo();
        string motorcycleInfo = motorcycle.GetInfo();
        
        // Assert
        Assert.Contains("Car", carInfo);
        Assert.Contains("Motorcycle", motorcycleInfo);
        Assert.NotEqual(carInfo, motorcycleInfo);
    }
    
    [Theory]
    [InlineData(typeof(Car), "Car")]
    [InlineData(typeof(Motorcycle), "Cruiser Motorcycle")]
    [InlineData(typeof(Truck), "Light Truck")]
    [InlineData(typeof(ElectricCar), "Electric Car")]
    public void Vehicle_VehicleType_ShouldReturnCorrectType(Type vehicleType, string expectedType)
    {
        // Arrange
        Vehicle vehicle = vehicleType.Name switch
        {
            nameof(Car) => new Car("Toyota", "Camry", 2024, 4),
            nameof(Motorcycle) => new Motorcycle("Harley", "Street", 2024, 
                                                MotorcycleType.Cruiser, 750),
            nameof(Truck) => new Truck("Ford", "F-150", 2024, TruckType.Light, 1000),
            nameof(ElectricCar) => new ElectricCar("Tesla", "Model 3", 2024, 4, 75, 500),
            _ => throw new ArgumentException("Unknown type")
        };
        
        // Act
        string type = vehicle.VehicleType;
        
        // Assert
        Assert.Equal(expectedType, type);
    }
    
    #endregion
}