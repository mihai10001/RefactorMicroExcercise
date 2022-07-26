using NUnit.Framework;
using Moq;

namespace TDDMicroExercises.TirePressureMonitoringSystem.Tests
{
    [TestFixture]
    public class AlarmTest
    {
        [Test]
        public void AlarmOn_field_should_be_false_on_Alarm_creation()
        {
            Alarm alarm = new Alarm();
            Assert.IsFalse(alarm.AlarmOn, "AlarmOn field should be set to false when creating a new Alarm");
        }

        [Test]
        public void AlarmOn_field_should_be_false_when_PSI_pressure_is_valid()
        {
            var _sensorMock = new Mock<Sensor>();
            _sensorMock.Setup(s => s.PopNextPressurePsiValue()).Returns(20);

            Alarm alarm = new Alarm(_sensorMock.Object);
            alarm.Check();

            Assert.IsFalse(alarm.AlarmOn, "AlarmOn field should be set to false when the tire PSI pressure value is between the accepted values.");
        }

        [Test]
        public void AlarmOn_field_should_be_true_when_PSI_pressure_is_below_threshold()
        {
            var _sensorMock = new Mock<Sensor>();
            _sensorMock.Setup(s => s.PopNextPressurePsiValue()).Returns(0);

            Alarm alarm = new Alarm(_sensorMock.Object);
            alarm.Check();

            Assert.IsTrue(alarm.AlarmOn, "AlarmOn field should be set to true when the tire PSI pressure value is not between the accepted values.");
        }

        [Test]
        public void AlarmOn_field_should_be_true_when_PSI_pressure_is_above_threshold()
        {
            var _sensorMock = new Mock<Sensor>();
            _sensorMock.Setup(s => s.PopNextPressurePsiValue()).Returns(100);

            Alarm alarm = new Alarm(_sensorMock.Object);
            alarm.Check();

            Assert.IsTrue(alarm.AlarmOn, "AlarmOn field should be set to true when the tire PSI pressure value is not between the accepted values.");
        }
    }
}
