# Test runner to be executed after building PLessPP
# This script expects to have defined a few things

param(
  [parameter(mandatory = $true)] [string] $VSTestConsolePath,
  [parameter(mandatory = $true)] [string] $WorkspacePath,
  [string[]] $TestAssemblies = @(
	'Test.Data.UnitTests\bin\Debug\PLessPP.Testing.Data.UnitTests.dll', 
	'Test.MWMSSearchAlgorithm.Tests\bin\Debug\PLessPP.Testing.MWMSSearchAlgorithm.Tests.dll', 
	'Test.Similarity.UnitTests\bin\Debug\PLessPP.Testing.Similarity.UnitTests.dll'
  )
);

$Options = @();
foreach ($TestAssembly in $TestAssemblies)
{
  $PathToTestAssembly = join-path -Path $WorkspacePath -ChildPath 'test' | 
						join-path -ChildPath $TestAssembly;
  $Options += $PathToTestAssembly;
}

# Testing
& $VSTestConsolePath $Options;

exit $LASTEXITCODE;