﻿using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectionBufferDemo
{
    internal interface IEditorFactory
    {
        IVsEditorFactory VsEditorFactory { get; } 
    }
}
