using System;
using System.Collections.Generic;
using System.Linq;
using Siemens.SimaticIT.Unified.Common;
using Siemens.SimaticIT.Unified.Common.Information;
using Siemens.SimaticIT.Handler;
using Siemens.SimaticIT.Unified;
using Siemens.SimaticIT.SDK.Diagnostics.Tracing;
using Siemens.SimaticIT.SDK.Diagnostics.Common;

namespace Siemens.Mom.Presales.Training.MasterData.DemoTimerLib.MSModel.Commands
{
    /// <summary>
    /// Partial class init
    /// </summary>
    [Handler(HandlerCategory.BasicMethod)]
    public partial class CreatePeriodicTimerHandlerShell 
    {
        /// <summary>
        /// This is the handler the MES engineer should write
        /// This is the ENTRY POINT for the user in VS IDE
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private CreatePeriodicTimer.Response CreatePeriodicTimerHandler(CreatePeriodicTimer command)
        {
            ITracer tracer = platform.Tracer;
            tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, "CreatePeriodicTimer Begin....");
            TimerContext timerContext = new TimerContext("DemoTimerApp");
            timerContext.GroupName = "AwakeningTimer";
            timerContext.AppContext = new ApplicationContext();

            timerContext.AppContext.UserField1 = "HaveATestonUserField1";

            //The following schedule will trigger the timer every hour
            //From 2019-01-01 00:00:00 to 2019-01-02 00:00:00 (CET)
            TimeSpan delay = new TimeSpan(0, 3, 0);
            PeriodicTimerSchedule periodicSchedule = new PeriodicTimerSchedule(delay);

            string timerUniqueName = "AwakeEvery10Seconds";

            try
            {
                Guid periodicTimerGuid = platform.CreateTimer(timerUniqueName, periodicSchedule, timerContext);
                if(periodicTimerGuid != null && !periodicTimerGuid.Equals(Guid.Empty))
                    return new CreatePeriodicTimer.Response { Id = periodicTimerGuid };
                else
                    return new CreatePeriodicTimer.Response (new ExecutionError(-1, $"Timer{ timerUniqueName },Timer creation failed due to a null create id."));
            }
            catch (UnifiedTimeSchedulerException ex)
            {
                switch (ex.PlatformCode)
                {
                    case 9002:
                        tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Error, $"Timer{ timerUniqueName },Timer creation failed due to a duplicated timer unique name.");
                        // Timer creation failed due to a duplicated timer unique name.
                        // Command handler execution can continue...
                        return new CreatePeriodicTimer.Response(new ExecutionError(-1, $"Timer{ timerUniqueName },Timer creation failed due to a duplicated timer unique name."));

                    case 9003:
                        // Timer creation failed due to input parameters validation.
                        // In this case it is safe to stop the execution of the command logic by
                        //returning an error response
                        tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Error, $"Timer{ timerUniqueName } cannot be created because { ex.Message}");
                        return new CreatePeriodicTimer.Response(new ExecutionError(-1, $"Timer{ timerUniqueName } cannot be created because { ex.Message}"));

                    default:
                        // If any other failure should occur, the command execution logic should bestopped
                        tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Error, $"Timer{ timerUniqueName } cannot be created because { ex.Message} ");
                        return new CreatePeriodicTimer.Response(new ExecutionError(-1, $"Timer{ timerUniqueName } cannot be created because { ex.Message} "));
                }
            }
            catch (UnifiedException exc)
            {
                tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Error, exc.Message);
                throw;
            }
        }
    }
}
