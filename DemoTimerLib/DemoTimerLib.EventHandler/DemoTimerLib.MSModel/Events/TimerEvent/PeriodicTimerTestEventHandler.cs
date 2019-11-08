using System;

using Siemens.SimaticIT.Unified.Common;
using Siemens.SimaticIT.Unified.Common.Information;
using Siemens.SimaticIT.Handler;

using Siemens.SimaticIT.SystemData.Foundation.Events;
using Siemens.Mom.Presales.Training.MasterData.DemoTimerLib.MSModel.Commands;
using Siemens.SimaticIT.SDK.Diagnostics.Tracing;
using Siemens.SimaticIT.SDK.Diagnostics.Common;

namespace Siemens.Mom.Presales.Training.MasterData.DemoTimerLib.MSModel.Events
{
    /// <summary>
    /// 
    /// </summary>
    [Handler(HandlerCategory.Event)]
    public partial class PeriodicTimerTestEventHandlerShell 
    {
        /// <summary>
        /// This is the Event handler the MES engineer should write
        /// This is the ENTRY POINT for the user in VS IDE
        /// </summary>
        /// <param name="evt"></param>
        /// <param name="envelope"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private void PeriodicTimerTestEventHandler (TimerEvent evt, EventEnvelope envelope)
        {
            ITracer tracer = platform.Tracer;
            try
            {
                tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, "PeriodicTimerTestEventHandler Begin....");
                tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, "envelope.Topic = " + envelope.Topic);
                tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, "envelope.Category = " + envelope.Category);
                tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, "envelope.Module = " + envelope.Module);
                tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, "envelope.Tag = " + envelope.Tag);

            }
            catch (Exception ex)
            {
                tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Error, ex.Message);
            }      
        }
    }
}
