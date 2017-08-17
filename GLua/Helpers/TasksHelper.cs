using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;

namespace GLua.Helpers
{
    /// <summary>
    /// Helps organize tasks
    /// </summary>
    class TasksHelper
    {
        ConcurrentDictionary<string,Task<AngleSharp.Dom.IDocument>> Tasks = new ConcurrentDictionary<string, Task<AngleSharp.Dom.IDocument>>();
        /// <summary>
        /// Add task to task list
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Task"></param>
        public async void AddTask(string Name,Task<AngleSharp.Dom.IDocument> Task)
        {
            await Tasks.AddOrUpdate(Name, Task, (key, oldValue) => Task);
        }
        /// <summary>
        /// Get result if task completed
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public AngleSharp.Dom.IDocument GetTaskResult(string Name)
        {
            if (Tasks.TryGetValue(Name, out Task<AngleSharp.Dom.IDocument> A))
            {
                return A.Result;
            }
            throw new ArgumentNullException(Name);
        }

    }
}
