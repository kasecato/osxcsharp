# Mac で C# の Web 開発ができるようになった!

　Visual Studio Code (VS Code) で C# を書いて，O/RM の Entity Framework Core (EFC) から PostgreSQL にアクセスし，データベースのレコードを ASP.NET Core で表示する。すべて macOS 上で可能になったのです。 そうです，Mac で ASP.NET Web アプリ開発がついにできるようになったのです！

　本稿では Mac 版 VS Code 上で C# を書き，EFC から Postgres に CRUD した結果を自動テストコード xUnit.net で検証します。

　Postgres のインストールと，VS Code のデバッグ，ASP.NET Core の Web アプリを配備できる非同期 I/O の Kestrel Web サーバについては本稿の対象外としています。僕が執筆中の薄い本をお待ち下さい。~~また，ほぼすべての技術が β 版（1.0.0-RC2 2016/5/20時点） 将来の変更で動作しなくなる可能性があります。~~ 2016/6/27 に ASP.NET Core 1.0 RTM （リリース版）となりました。メジャー バージョンの変更がない限り，今後は大幅な仕様変更がないと思われます・・・。（開発者は仕様変更がないホットなフレームワークが存在しないことを知っています）

![NETCore.png](https://cloud.githubusercontent.com/assets/10364603/15401835/44f5992c-1e2c-11e6-83af-93c1506babeb.jpg)

# Mac C# の歴史

　Javascript (JS) を ~~JS おじさんが~~ ECMAScript (エクマ スクリプト) と呼ぶことがありますが，これは国際的な標準化団体である「Ecma インターナショナル」(1961年) の <a href="http://www.ecma-international.org/publications/standards/Ecma-262.htm" target="_blank">ECMA-262</a> (1997年) として JS が標準化されているためです。

　C# が JS と同様に <a href="http://www.ecma-international.org/publications/standards/Ecma-334.htm" target="_blank">ECMA-334</a> (2000年) として標準化されていることは ~~C# おじさんを除いて~~ あまり知られていないことかもしれません。

## Visual C# (Windows)

　C# 標準の言語実装は <a href="https://ja.wikipedia.org/wiki/%E3%82%A2%E3%83%B3%E3%83%80%E3%83%BC%E3%82%B9%E3%83%BB%E3%83%98%E3%83%AB%E3%82%B9%E3%83%90%E3%83%BC%E3%82%B0" target="_blank">アンダース・ヘルスバーグ</a> (Typescript のお父さん) による Microsoft <a href="https://msdn.microsoft.com/ja-jp/library/kx37x362.aspx" target="_blank">Visual C#</a> (2000年，2002年) が最も有名です。C# といえば多くの文脈で 「Windows 上の Visual Studio で開発される Visual C#」を意味していました。

　C# の Web 開発 = Windows = **商用非公開** （安心のベンダー サポートは付く）エンタープライズ開発の印象が強く，オープンで活発なコミュニティを基盤に，ドキュメントやナレッジ コミュニティが充実した OSS が主流の Web 界隈とは長らく一線を画していたように思います。

　*生き残るのに大事なこと = 圧倒的金か愛が感じられるもの*
 - <a href="http://www.slideshare.net/MasashiSakurai/javascript-js-53219222" target="_blank">JavaScript の過去と現在、ガチな JS アプリケーション設計</a>

## Mono (Linux，Mac，Windows，Android，iOS)

　C# 標準の言語実装は Microsoft の Visual C# だけではありません。Linux，Windows，そして macOS で動作する <a href="https://ja.wikipedia.org/wiki/%E3%83%9F%E3%82%B2%E3%83%AB%E3%83%BB%E3%83%87%E3%83%BB%E3%82%A4%E3%82%AB%E3%82%B6" targe="_blank">ミゲル・デ・イカザ</a> による <a href="http://www.mono-project.com/", target="_blank">Mono</a> (2001年) (スペイン語で猿) があります。

　Mono には Visual Studio ~~級~~ライクの統合開発環境 (IDE) <a href="http://www.monodevelop.com/" target="_blank">MonoDevelop</a> も存在し，クロス プラットフォームでリッチな C# の開発が可能となっています。個人的には XAML 開発は Mono よりも VS の操作性の方が好みです。

　モバイルの分野では，Mono から派生した Xamarin （ザマリン）（2011年）や Unity (2005年) の登場で，C# による Android，iOS のネイティブ／ゲーム アプリも開発できるようになりました。

　Xamarin は 本家 Visual Studio 2015 より標準でインストールできるようになり，Android のデザイナー，デバッガ，エミュレータまでも VS 2015 にはあります。<a href="http://www.idc.com/prodserv/smartphone-os-market-share.jsp" target="_blank">モバイル分野で後塵を拝した Microsoft</a> が本気になっているようです。

<a href="http://www.slideshare.net/chack411/net-core-5-windows-linux-os-x-docker" targe="_blanl">![VS2015.png](https://qiita-image-store.s3.amazonaws.com/0/67778/c472feec-9f9a-a24f-6fe5-8fa1d3d227f6.png)</a>

## .NET Core (Linux，Mac，Windows)

　近年の Web 開発は，JS を中心とした開発の生産性や性能の向上に挑戦し続けており，新しい技術が凄まじいスピードで誕生し続けています。特にフロントエンド技術の変遷は<a href="http://postd.cc/longevity-or-lack-thereof-in-javascript-frameworks" target="_blank">「JavaScriptフレームワークの寿命」</a>でも書かれたように，2012年より毎年フレームワークのトレンドが移り変わるという状態です。渦中の Web おじさんとしては，毎日が新しい挑戦でこれが想像以上に楽しいです。

　モダン Web 開発の例としては，バックエンドにイベント駆動型の Node.js，フロントエンドは仮想 DOM の React や双方向バインディングの AngularJS，データベースはスケールアウトを前提とした Cassandra や MongoDB，パッケージ管理に npm, yarn, Browserify や Bower，ビルドシステムは gulp や Grunt，VSC (Version Control System) に Git を使い，CSS は PostCSS や Sass で書き stylelint で lint，CircleCI や Travis CI で継続的にビルドもする。

　対話形式でプロジェクトや画面のひな形を生成するスキャフォールディング（足場を組む）の Yeoman （ヨーマン）で MongoDB，Express，Angular，Node のスケルトン コードを生成させる，つまり Web 開発を JS でフルスタックに開発する <a href="http://meanjs.org/" target="_blank">"MEAN スタック"</a> が SPA (Single Page Application) の登場とともに有名になりました。

　このようなモダン Web 開発フローのひとつの選択肢として，バックエンドに C# も気軽に選択できるようにしたのが .NET Core をベースとする ASP.NET Core が登場したひとつの背景だと思います。.NET Core はすべてオープン ソースで，クロス プラットフォームであり，1 つのコードで Linux のサーバでも，Mac でも，Windows でも動作します。素晴らしいです。

![NETCore.png](https://cloud.githubusercontent.com/assets/10364603/15401733/d0bf0bc4-1e2b-11e6-9ca4-e20bbf53bbde.jpg)

# VS Code (Linux, Mac, Windows)

　Visual Studio Code (VS Code) は Linux，Mac，Windows 上で無料で利用できる ~~<a href="https://atom.io/" target="_blank">Atom</a>~~ <a href="http://electron.atom.io/" target="_blank">Electron</a> を拡張したテキスト エディターです。テキスト エディターですが，実際に C# の開発に利用してみると，本家 IDE の Windows Visual Studio に匹敵する機能があり，本格的な C# 開発に耐えられるレベルの機能が備わっていると感じます。

　VS Code は導入も動作も軽量で素晴らしく，必要なモジュールのみをパッケージ管理 NuGet (ヌゲット，ニューゲット) で取得するスタイルは，**モダンな Web 開発のフローに慣れた Web 開発者にとても親しみやすく構成されている**と思います。

　逆に，従来の Windows Visual Studio が提供するリッチな GUI サポートで Web 開発に慣れた開発者にとっては，VS Code は 近年の移り変わりの激しい Web 開発技術の学習コストが高い割に，生産性が低く難解なもののように感じるのではないかと思われます。

　Windows 版 Visual Studio の生産性を macOS で追求するには，2016 年 11 月 17 日 にプレビュー版がリリースされた [Visual Studio for Mac](https://www.visualstudio.com/vs/visual-studio-mac/) をぜひ使用してみてください。（これから本稿で紹介するプロジェクト構成の知識をすべて VS for Mac が吸収しますので，開発者はプロダクトに集中することができます。）

　脱線しましたが，VS Code で実際に C# 開発を検証した結果をまとめました。

## VS Code で出来たこと (一部)

* IntelliSense (コード自動補完) (<a href="http://www.omnisharp.net/" target="_blank">OmniSharp</a>)
* 修正候補 (いわゆる電球)
* 入力候補
* メンバーの一覧
* クイック ヒント
* パラメータ ヒント
* スニペット
* エラー，警告の即時表示
* ドキュメントのフォーマット(コード整形)
* すべての参照の検索
* 定義に移動
* リファクタリング
* using の補完
* 未使用の using を整理
* ピークの定義を表示する
* ターミナルを開く
* Markdown のプレビュー
* Git
* 変更箇所の表示
* 変更を元に戻す
* パッケージ変更を検知によるパッケージの自動取得
* キー バインド カスタマイズ
* テーマ カスタマイズ
* <a href="https://code.visualstudio.com/Docs/editor/debugging" target="_blank">デバッグ</a>

## VS Code で出来なかったこと (一部)

* GUI のデザイナー (XAML，モデル等)
* プロファイラー

## VS Code インストール

　<a href="https://code.visualstudio.com/" target="_blank">Visual Studio Code</a> より，macOS 版をダウンロードしてください。 

# 環境構築

　Java の開発に JDK，Javascript の開発に node や npm が必要なように，macOS の C# 開発には **.NET Core ツール（dotnet）** が必要となります。

## .NET Core ツール / dotnet

　.NET Core ツール（dotnet）はパッケージ管理や .NET アプリケーションをビルド&実行&テストするためのツールです。

### OpenSSL

　インストールには OpenSSL が必要です。

```bash
brew update
brew install openssl
mkdir -p /usr/local/lib
ln -s /usr/local/opt/openssl/lib/libcrypto.1.0.0.dylib /usr/local/lib/
ln -s /usr/local/opt/openssl/lib/libssl.1.0.0.dylib /usr/local/lib/
```

### .NET Core Installer macOS

　最後に<a href="https://www.microsoft.com/net/download/core" target="_blank">.NET Core</a> macOS 版をダウンロードして，パッケージをインストールします。

　以上で，環境構築が完了です。

# 開発

　Hello World といえば，コンソールに Hello World を出力することです。<a href="http://rosettacode.org/wiki/Category:Programming_Languages" target="_blank">Rosetta Code には 595 の 言語で Hello World</a> が刻まれています。

　Hello World をファイルに書き込んで，出力することが Hello World 1.5 とすると，Hello World 2.0 は Star Wars の監督を Web 出力することなのかもしれません。 

## Hello World 2.0 テスト プロジェクト構成

```bash
StarWars
├── project.csproj
├── src
│   ├── Domain
│   │   └── StarWarsContext.cs
│   ├── Model
│   │   └── Director.cs
│   └── Program.cs
└── test
    └── StarWarsTest.cs
```

| ファイル／フォルダ     | 役割              | 説明             |
|:--------------------|:------------------|:----------------|
| StarWars            | プロジェクトのルート フォルダ | 実行時のエントリー ポイント名となります |
| project.csproj      | パッケージ管理ファイル (NuGet 用) | npm の package.js に相当します。**VS Code は本ファイルを検知して OmniSharp (コード補完機能等) を有効化します**。 |
| StarWarsContext.cs  | EFC Npgsql 用 DB 接続情報 | Postgres の DB 接続情報を記述します。DbContext と呼びます |
| Director.cs         | EFC 用のテーブル／モデル | Postgres のテーブルに対応するモデルです |
| StarWarsCrudTest.cs | xUnit.net 用の自動テストコード | テストファイルです |


## NuGet パッケージ管理 

　<a href="https://msdn.microsoft.com/ja-jp/library/dn878908%28v=vs.110%29.aspx" targe="_blank">*.NET Core は，小規模のアセンブリ パッケージで NuGet を介してリリースされるためモジュール形式となっています。</a>*

　Hello World 2.0 で必要なパッケージは NuGet で入手します。json を書いてコマンドを実行すると，プロジェクトに必要なパッケージの依存関係を自動で解決してくれます。バージョンアップも 1 コマンドです。VS Code であれば ~~package.json~~ project.json を変更して保存すると自動でパッケージの取得ダイアログを表示してくれるので便利です。


| パッケージ    | 役割              | 説明             |
|:-------------------|:------------------|:----------------|
| <a href="https://docs.microsoft.com/en-us/ef/" target="_blank">Entity Framework Core</a> | O/RM | CRUD やトランザクションの制御だけではなくテーブルの自動作成やマイグレーションも可能です | 
| <a href="http://www.npgsql.org/" target="_blank">Npgsql</a> | EFC Postgres データ プロバイダー | EFC で Postgres への接続を可能にします |
| <a href="https://xunit.github.io/docs/getting-started-dotnet-core.html" target="_blank">xUnit.net</a> | 自動テストコード | macOS の ASP.NET Core でも実行可能なユニット テスト フレームワークです |

### project.json

　NuGet によるパッケージ管理ファイルは以下のとおりです。

```xml:project.csproj
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <AssemblyName>StarWars</AssemblyName>
    <PackageId>StarWars</PackageId>
    <VersionPrefix>1.0.0</VersionPrefix>
    <TargetFramework>netcoreapp2.0</TargetFramework>
    <OutputType>Library</OutputType>
    <DebugType>portable</DebugType>
    <GenerateDocumentationFile>false</GenerateDocumentationFile>
    <GenerateRuntimeConfigurationFiles>true</GenerateRuntimeConfigurationFiles>
    <RuntimeFrameworkVersion>2.0.0</RuntimeFrameworkVersion>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="15.3.0" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.2.0" />
    <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="2.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="2.0.0">
      <PrivateAssets>All</PrivateAssets>
    </PackageReference>
    <PackageReference Include="xunit" Version="2.2.0" />
    <PackageReference Include="Microsoft.Extensions.PlatformAbstractions" Version="1.1.0" />
  </ItemGroup>

  <ItemGroup>
    <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0" />
  </ItemGroup>

</Project>
```

　NuGet でパッケージを取得するコマンドは以下の通りです。

```bash
dotnet restore
```

## Entity Framework Core

　DB のテーブルをプログラムでモデリングすると，モデルの定義に従って自動で DB のテーブルが生成され，しかもプログラムのモデルを変更すると DB のテーブルもマイグレーションすることが可能となるパッケージが Entity Framework の開発手法「コード ファースト」です。もちろん DB からモデルを生成する「データベース ファースト」開発にも EFC は対応しています。 

### Model/Director.cs

　Postgres の テーブルに紐づく EFC のモデルは，以下のとおりです。

```csharp:Director.cs
using System;

namespace StarWars.Model
{
    public class Director
    {
        public int DirectorId { get; set; }
        public int Episode    { get; set; }
        public string Name    { get; set; }
    }
}
```

　モデルにアトリビュート (Java のアノテーションに相当) の記述が一切ありません。これは EF が設定を最小限にする思想 CoC (Convention over Configuration: 設定より規約) であるためです。規約に沿ったプログラムさえ書けば，面倒な設定はフレームワークがよしなに設定してくれます。本モデルでは テーブル名 + Id の DirectorId が自動的に主キーとして自動設定されます。

### Domain/StarWarsContext.cs

　EFC から Postgres にアクセスするための接続情報 (DbContext) は以下の通りです。

```csharp:StarWarsContext.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.PlatformAbstractions;
using StarWars.Model;

namespace StarWars.Domain
{
    public class StarWarsContext : DbContext
    {
        public DbSet<Director> Directors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = PlatformServices.Default.Application.ApplicationBasePath;
            optionsBuilder.UseNpgsql("Host=localhost;Username=c3po;Password=r2d2;Database=starwars");
        }
    }
}
```

### EFC マイグレーション

　DbContext とモデルからマイグレーションに必要なプログラムを自動生成するコマンドは以下のとおりです。

```bash
dotnet ef migrations add StarWarsEp7
```

　上記コマンドで Migrations フォルダが作成され，Migrations フォルダの中に自動生成されたマイグレーション プログラムの断面が保存され続けます。

　Postgres にデータベースとテーブルを反映するコマンドは以下のとおりです。

```bash
dotnet ef database update
```

![EFCMigrationsUpdate.png](https://qiita-image-store.s3.amazonaws.com/0/67778/b4127220-5c67-7350-8a2e-5885698a9877.png)

　モデリングしたとおりに starwars データベースと Director テーブルが作成されました。EFC 管理用の __EFMigrationsHistory も作成されています。

　update コマンドで注意することは，一度 Postgres に update したあとは，モデルの断面を決して削除しない（migrations remove コマンドは使えない）ことです。テーブルを変更したい場合は，コードのモデルを変更し，migrations add コマンドからやり直します。もちろん最初のマイグレーションとは別名にします。

　モデル Director.cs に Born を追加してみましょう。

```csharp:Director.cs
using System;

namespace StarWars.Model
{
    public class Director
    {
        public int DirectorId { get; set; }
        public int Episode    { get; set; }
        public string Name    { get; set; }
        public DateTime Born  { get; set; } // 追加
    }
}
```

　最初とは別名でマイグレーションを実行します。

```bash
dotnet ef migrations add StarWarsEp7.2
dotnet ef database update
```

![EFCMigrationsUpdate2.png](https://qiita-image-store.s3.amazonaws.com/0/67778/b0b7fe02-715d-fc58-3c7d-e8614ea1ea20.png)


　追加した項目 Born が追加されました。マイグレーションしたテーブルにすでにデータが存在する場合は，自動で初期値が設定されます。もちろんアトリビュートを使えば初期値，主キーや文字列長など細かく設定できます。

### Test/StarWarsCrudTest.cs

　xUnit.net を使って EFC から Postgres に Select，Delete，Insert するテストコードは以下のとおりです。

```csharp:StarWarsCrudTest.cs
using System;
using StarWars.Domain;
using StarWars.Model;
using Xunit;

namespace StarWars.Test
{
    public class StarWarsCrudTest
    {
        [Fact]
        public void CreateTest()
        {
            using (var db = new StarWarsContext())
            {
                // arrange
                var dirEp7 = new Director()
                {
                    DirectorId = 4,
                    Episode = 7,
                    Name = "J. J. Abrams",
                    Born = DateTime.Parse("June 27, 1966")
                };

                // act
                // SELECT
                var dirEp7Db = db.Directors.Find(dirEp7.DirectorId);

                // DELETE
                if (dirEp7Db != null)
                {
                db.Directors.Remove(dirEp7Db);
                db.SaveChanges();
                }

                // INSERT
                db.Directors.Add(dirEp7);
                var countIns = db.SaveChanges();

                // SELECT
                dirEp7Db = db.Directors.Find(dirEp7.DirectorId);

                // assert
                Assert.Equal(1, countIns);
                Assert.Equal("J. J. Abrams", dirEp7Db.Name);
            }
        }
    }
}
```

　テストコードを実行するコマンドは以下のとおりです。

```bash
dotnet test
```

　実行結果

```bash
bash$ dotnet test
Microsoft (R) Test Execution Command Line Tool Version 15.0.0.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
[xUnit.net 00:00:00.6235341]   Discovering: StarWars
[xUnit.net 00:00:00.7345812]   Discovered:  StarWars
[xUnit.net 00:00:00.8056868]   Starting:    StarWars
[xUnit.net 00:00:02.0368794]   Finished:    StarWars

Total tests: 1. Passed: 1. Failed: 0. Skipped: 0.
```

![EFCCreate.png](https://qiita-image-store.s3.amazonaws.com/0/67778/24be3cf9-3c95-f2a3-2b45-50fce22ca40b.png)



# まとめ
 
* macOS 版 VS Code でも 本家 Windows VS に匹敵する C# 開発ができた
* NuGet を使用したパッケージ管理ができた
* EFC のコード ファーストが実践できた
* EFC で Postgres がマイグレーションできた
* EFC で Postgres に C R~~U~~D できた
* xUnit.net で 自動テストコードが書けた
* ~~Kestrel Web サーバ上に ASP.NET Core を配備して Star Wars ができた~~


# 付録

　今回のソースコードを GitHub に掲載しました。ご利用ください。

　GitHub, "osxcsharp", https://github.com/k--kato/osxcsharp/

## ASP.NET 5 is dead (2016/1/19)

　[大規模な名前変更](http://www.hanselman.com/blog/ASPNET5IsDeadIntroducingASPNETCore10AndNETCore10.aspx)がありましたので，補足します。

> * ASP.NET 5 is now ASP.NET Core 1.0. 
> * .NET Core 5 is now .NET Core 1.0.
> * Entity Framework 7 is now Entity Framework Core 1.0 or EF Core 1.0 colloquially. 


## DNX is dead (2016/5/20)

　[RC1 から RC2 のバージョンアップに伴い大規模な仕様変更](https://blogs.msdn.microsoft.com/dotnet/2016/05/16/announcing-net-core-rc2/)がありましたので，補足します。

RC1 | RC2
----|-----
dnu restore | dotnet restore
dnx build   | dotnet build
dnx run     | dotnet run
dnx test    | dotnet test
dnx ef      | dotnet ef
EntityFramework7.Npgsql        | Npgsql.EntityFrameworkCore.PostgreSQL
EntityFramework7.Npgsql.Design | Npgsql.EntityFrameworkCore.PostgreSQL.Design
xunit.runner.dnx | dotnet-test-xunit

## Announcing ASP.NET Core 1.0 (2016/6/27)

　ASP.NET Core 1.0 RTM (release to manufacturing: リリース版) となりました！（そして私が MVP の審査で落選の通知を受け取りました）

## Npgsql 3.1.8 and Npgsql EFCore 1.0.2 are out (2016/9/23)

　Entity Framework Core 1.0.2 がリリースされ，Npgsql が 3.1.8 となりました。

## Announcing .NET Core 1.1, Announcing Entity Framework Core 1.1 (2016/11/16)

　.NET Core 1.1.0 と Entity Framework Core 1.1.0 がリリースされました。

　1.1.0 以前の環境が構築されていて，dotnet restore で porject.json のエラーが発生する場合は，以下の[手順](http://iamnotmyself.com/2016/06/27/installing-net-core-1-0-on-a-dirty-osx/)を参考にしてください。

```bash
sudo nuget update -self
nuget locals all -clear
sudo rm -rf /usr/local/share/dotnet/
# 最新の .NET Core 1.1 SDK をインストールする
```

　dotnet ef コマンドが使用できない場合は，project.json ファイルの `emitEntryPoint` を `true` に設定して，project がコンソール アプリであることを明記することで解決します。 

## .NET Core プロジェクトから .csproj 形式への移行 (2017/3/13)

[.NET Core は project.json から .csproj へ移行しています](https://docs.microsoft.com/ja-jp/dotnet/articles/core/migration/)。
project.json を .csproj にマイグレーションするコマンドは以下のとおりです。

```bash
dotnet migrate
```

## Announcing .NET Core 2.0, Announcing Entity Framework Core 2.0 (2017/8/14)

　.NET Core 2.0.0 と Entity Framework Core 2.0.0 がリリースされました。


# 参考ノート

 1. Entity Framework Documentation, "Getting Started on OS X", https://docs.efproject.net/en/latest/platforms/coreclr/getting-started-osx.html
 1. Npgsql, "Entity Framework Core", http://www.npgsql.org/doc/efcore.html
 1. xUnit.net Documentation, "Getting started with xUnit.net (.NET Core / ASP.NET Core)", https://xunit.github.io/docs/getting-started-dotnet-core.html
 1. Akira Inoue, 「.NET Core 5 ～ Windows, Linux, OS X そして Docker まで ～」, http://www.slideshare.net/chack411/net-core-5-windows-linux-os-x-docker
 1. 岩永信之，山田祥寛，井上章，伊藤伸裕，熊家賢治，神原淳史　著『C#エンジニア養成読本』技術評論社
 1. Visual Studio Code, "Working with C#", https://code.visualstudio.com/Docs/languages/csharp
 1. Visual Studio Code, "User and Workspace Settings", https://code.visualstudio.com/docs/customization/userandworkspace
 1. Visual Studio Code, "Debugging", https://code.visualstudio.com/docs/editor/debugging
 1. Visual Studio Code, "Editing Evolved", https://code.visualstudio.com/docs/editor/editingevolved
 1. Scott Hanselman, "ASP.NET 5 is dead - Introducing ASP.NET Core 1.0 and .NET Core 1.0", http://www.hanselman.com/blog/ASPNET5IsDeadIntroducingASPNETCore10AndNETCore10.aspx
 1. .NET Core, "Migrating from DNX to .NET Core CLI
Overview", http://dotnet.github.io/docs/core-concepts/dnx-migration.html
 1. .NET Web Development and Tools Blog, "Announcing ASP.NET Core 1.0", https://blogs.msdn.microsoft.com/webdev/2016/06/27/announcing-asp-net-core-1-0/
 1. Npgsql, "Npgsql 3.1.8 and Npgsql EFCore 1.0.2 are out", http://www.npgsql.org/news/npgsql-3.1.8-efcore-1.0.2.html
 1. I Am NotMyself, "Installing .NET Core 1.0 on a Dirty OSX", http://iamnotmyself.com/2016/06/27/installing-net-core-1-0-on-a-dirty-osx/
 1. Ngpsql, "Getting Started", http://www.npgsql.org/efcore/index.html
 1. Microsoft, "Entity Framework Core", https://docs.microsoft.com/en-us/ef/core/index
 1. .Net Blog, "Announcing .NET Core 1.1", https://blogs.msdn.microsoft.com/dotnet/2016/11/16/announcing-net-core-1-1/
 1. .NET Blog, "Announcing Entity Framework Core 1.1", https://blogs.msdn.microsoft.com/dotnet/2016/11/16/announcing-entity-framework-core-1-1/
 1. Microsoft Docs, 「project.json プロパティと csproj プロパティの間のマッピング」, https://docs.microsoft.com/ja-jp/dotnet/articles/core/tools/project-json-to-csproj
 1. .NET Blog, "Announcing .NET Core 2.0", https://blogs.msdn.microsoft.com/dotnet/2017/08/14/announcing-net-core-2-0/
 