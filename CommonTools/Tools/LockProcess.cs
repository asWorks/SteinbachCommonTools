using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CommonTools.Tools
{
    public class LockProcess
    {
        public static enumResultLockProcess DoLockProcess(Func<bool> FuncName, string key, int repeat = 5, int TimeoutMilliseconds = 500)
        {
            bool Success = false;
            int counter = 0;
            enumResultLockProcess Result = enumResultLockProcess.Init;

            while (counter < repeat && Success == false)
            {
                using (var db = new SteinbachEntities())
                {
                    config res = db.config.Where(n => n.mkey == key).SingleOrDefault();
                    if (res == null)
                    {
                        res = LockProcess.addKeyIfMissing(db, key);
                        if (res == null)
                        {
                            Success = false;
                            Result = enumResultLockProcess.KeyNotFoundAndUnableToBeAdded;
                        }

                    }

                    if (res != null && res.value == "0")
                    {
                        res.value = "1";
                        db.SaveChanges();

                        Success = true;
                        Result = FuncName() == false ? enumResultLockProcess.FunctionReportsFalse : enumResultLockProcess.Succes;
                    }

                }
                counter++;
                LockProcess.wait(new TimeSpan(0, 0, 0, 0, TimeoutMilliseconds));
            }

            if (!Success)
            {
                if (Result != enumResultLockProcess.KeyNotFoundAndUnableToBeAdded)
                {
                    Result = enumResultLockProcess.TimeOutWhileTryToLock;
                }
            }

            if (Success)
            {
                using (var db = new SteinbachEntities())
                {
                    config res = db.config.Where(n => n.mkey == key).SingleOrDefault();
                    res.value = "0";
                    db.SaveChanges();
                }
            }


            return Result;


        }






        private static void wait(TimeSpan ts)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (sw.Elapsed.CompareTo(ts) < 0) { }


        }



        public enum enumResultLockProcess
        {
            FunctionReportsFalse,
            TimeOutWhileTryToLock,
            Succes,
            Init,
            KeyNotFoundAndUnableToBeAdded

        }


        private static config addKeyIfMissing(SteinbachEntities Context, string key)
        {

            try
            {

                var res = new config();
                res.mkey = key;
                res.value = "0";
                Context.AddToconfig(res);
                Context.SaveChanges();
                return res;


            }
            catch (Exception)
            {

                return null;
            }

        }
    }
}
