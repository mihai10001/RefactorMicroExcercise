using NUnit.Framework;

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
    }
}
