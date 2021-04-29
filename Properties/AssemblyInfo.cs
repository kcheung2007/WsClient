using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Resources;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("WsClient")]
[assembly: AssemblyDescription( "QA Tool" )]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany( "Autonomy" )]
[assembly: AssemblyProduct("WsClient")]
[assembly: AssemblyCopyright( "Copyright © QA Autonomy 2009" )]
[assembly: AssemblyTrademark( "Autonomy" )]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM componenets.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible( false )]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("d07a6a8d-99c3-4052-8b80-bfe1b1b30b6b")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
[assembly: AssemblyVersion( "1.0.*" )]

// Setting min. security to resolve warning CA2209
[assembly: SecurityPermissionAttribute( SecurityAction.RequestMinimum, Execution = true )]

[assembly: CLSCompliant( true )]
[assembly: NeutralResourcesLanguageAttribute( "en" )]
namespace WsClient { }
