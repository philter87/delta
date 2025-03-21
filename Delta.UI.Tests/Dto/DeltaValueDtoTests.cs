using System.Text.Json;
using Delta.UI.Dto;
using FluentAssertions;

namespace Delta.UI.Tests.Dot;

public class DeltaValueDtoTests
{
    [Fact]
    public void From_Should_ReturnDto()
    {
        // Arrange
        var deltaValue = new DeltaValue<int>("test", 42);
        
        // Act
        var dto = DeltaValueDto.From(deltaValue);
        
        // Assert
        dto.Name.Should().Be(deltaValue.Name);
        dto.Value.Should().Be(deltaValue.Value);
        dto.TypeName.Should().Be(typeof(int).FullName);
    }
    
    [Fact]
    public void Should_SerializeIntDeltaValue()
    {
        // Arrange
        var dValue1 = new DeltaValue<int>("test", 42);
        
        // Act
        var dValue2 = SerializeAndDeserialize(dValue1);
        
        // Assert
        dValue1.Name.Should().Be(dValue2.Name);
        dValue1.GetValue().Should().Be(dValue2.GetValue());
    }
    
    [Fact]
    public void Should_SerializeString()
    {
        // Arrange
        var dValue1 = new DeltaValue<string>("test", "bla");
        
        // Act
        var dValue2 = SerializeAndDeserialize(dValue1);
        
        // Assert
        dValue1.GetValue().Should().Be(dValue2.GetValue());
    }
    
    [Fact]
    public void Should_SerializeDateTimeOffset()
    {
        // Arrange
        var date = new DateTimeOffset(2025, 3, 20, 23, 0, 0, TimeSpan.Zero);
        var dValue1 = new DeltaValue<DateTimeOffset>("test", date);
        
        // Act
        var dValue2 = SerializeAndDeserialize(dValue1);
        
        // Assert
        dValue2.GetValue().Should().Be(dValue1.GetValue());
    }
    
    [Fact]
    public void Should_SerializeDictionary()
    {
        // Arrange
        var dict = new Dictionary<string, string>() { { "key1", "value1" } };
        var dValue1 = new DeltaValue<Dictionary<string, string>>("test", dict);
        
        // Act
        var dValue2 = SerializeAndDeserialize(dValue1);
        
        // Assert
        var dict2 = dValue2.GetValue() as Dictionary<string, string>;
        dict2!["key1"].Should().Be(dict["key1"]);
    }
    
    private static DeltaValue SerializeAndDeserialize(DeltaValue deltaValue)
    {
        var dto1 = DeltaValueDto.From(deltaValue);
        var json1 = JsonSerializer.Serialize(dto1);
        var dto2 = JsonSerializer.Deserialize<DeltaValueDto>(json1);
        return dto2.ToDeltaValue();
    }

    private static string Serialize(DeltaValue deltaValue)
    {
        var dto = DeltaValueDto.From(deltaValue);
        return JsonSerializer.Serialize(dto);
    }
}