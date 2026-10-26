{
  description = ".NET 10 ASP.NET Core development shell";

  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-unstable";
    flake-utils.url = "github:numtide/flake-utils";
  };

  outputs = { self, nixpkgs, flake-utils }:
    flake-utils.lib.eachDefaultSystem (system:
      let
        pkgs = import nixpkgs {
          inherit system;
        };

        dotnet = pkgs.dotnetCorePackages.sdk_10_0;
      in
      {
        devShells.default = pkgs.mkShell {
          packages = with pkgs; [
            dotnet
            git
            gh
            opencode
            curl
            jq
          ];

          DOTNET_ROOT = "${dotnet}";

          # Avoid writing first-run messages on every new shell.
          DOTNET_NOLOGO = "true";
          DOTNET_CLI_TELEMETRY_OPTOUT = "true";

          shellHook = ''
            export DOTNET_CLI_HOME="$HOME/.dotnet"
            export NUGET_PACKAGES="$HOME/.nuget/packages"

            mkdir -p "$DOTNET_CLI_HOME" "$NUGET_PACKAGES"

            echo ".NET development shell"
            dotnet --version
          '';
        };
      });
}
