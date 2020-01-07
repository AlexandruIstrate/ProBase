﻿using ProBase.Utils;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ProBase.Tests.Generation
{
    public class GenerationUtils
    {
        public static TypeBuilder GetTypeBuilder(Type interfaceType)
        {
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(TypeNames.GetAssemblyName()), AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("ModuleName");

            TypeBuilder typeBuilder = moduleBuilder.DefineType(TypeNames.GenerateFullClassName("TestType"), TypeAttributes.Public | TypeAttributes.Class);
            typeBuilder.AddInterfaceImplementation(interfaceType);

            return typeBuilder;
        }
    }
}
