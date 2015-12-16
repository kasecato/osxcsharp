# Mac で C# の Web 開発ができるようになった!

　Visual Studio Code (VS Code) で C# を書いて，O/RM の Entity Framework 7 (EF7) から PostgreSQL にアクセスし，データベースのレコードを ASP.NET 5 で表示する。すべて Mac OS X 上で可能になったのです。 そうです，Mac で ASP.NET Web サービス開発がついにできるようになったのです！

　本稿では Mac 版 VS Code 上で C# を使い， EF7 から Postgres に CRUD した結果を自動テストコード xUnit.net で検証します。最後に ASP.NET 5 を使い DB のデータを出力する Web サービスを Mac OS X 上で起動させます。（今日中に，たぶん・・・）

　Postgres のインストールと，VS Code のデバッグについては本稿の対象外としています。また，ほぼすべての技術が β 版のため，将来の変更で動作しなくなる可能性があります。

# Mac C# の歴史

　Javascript (JS) を ~~JS おじさんが~~ ECMAScript (エクマ スクリプト) と呼ぶことがありますが，これは国際的な標準化団体である「Ecma インターナショナル」(1961年) の <a href="http://www.ecma-international.org/publications/standards/Ecma-262.htm" target="_blank">ECMA-262</a> (1997年) として JS が標準化されているためです。

　C# が JS と同様に <a href="http://www.ecma-international.org/publications/standards/Ecma-334.htm" target="_blank">ECMA-334</a> (2000年) として標準化されていることは ~~C# おじさんを除いて~~ あまり知られていないことかもしれません。

## Visual C# (Windows)

　C# 標準の言語実装は <a href="https://ja.wikipedia.org/wiki/%E3%82%A2%E3%83%B3%E3%83%80%E3%83%BC%E3%82%B9%E3%83%BB%E3%83%98%E3%83%AB%E3%82%B9%E3%83%90%E3%83%BC%E3%82%B0" target="_blank">アンダース・ヘルスバーグ</a> (TypeScript のお父さん) による Microsoft <a href="https://msdn.microsoft.com/ja-jp/library/kx37x362.aspx" target="_blank">Visual C#</a> (2000年，2002年) が最も有名です。C# といえば多くの文脈で 「Windows 上の Visual Studio で開発される Visual C#」を意味していました。

　C# の Web 開発 = Windows = **商用非公開** （安心のベンダーサポートは付く）エンタープライズ開発の印象が強く，オープンで活発なコミュニティを基盤に，ドキュメントやナレッジ コミュニティが充実した OSS が主流の Web 界隈とは長らく一線を画していたように思います。

　*生き残るのに大事なこと = 圧倒的金か愛が感じられるもの*
 - <a href="" target="_blank">JavaScript の過去と現在、ガチな JS アプリケーション設計</a>

## Mono (Linux，Mac，Windows，Android，iOS)

　C# 標準の言語実装は Microsoft の Visual C# だけではありません。Linux，Windows，そして Mac OS で動作する <a href="https://ja.wikipedia.org/wiki/%E3%83%9F%E3%82%B2%E3%83%AB%E3%83%BB%E3%83%87%E3%83%BB%E3%82%A4%E3%82%AB%E3%82%B6" targe="_blank">ミゲル・デ・イカザ</a> による <a href="http://www.mono-project.com/", target="_blank">Mono</a> (2001年) (スペイン語で猿) があります。

　Mono には Visual Studio 級の統合開発環境 (IDE) <a href="http://www.monodevelop.com/" target="_blank">MonoDevelop</a> も存在し，クロス プラットフォームでリッチな C# の開発が可能となっています。個人的には XAML 開発は Mono よりも VS の操作性の方が好みです。

　モバイルの分野では，Mono から派生した Xamarin （ザマリン）（2011年）や Unity (2005年) の登場で，C# による Android，iOS のネイティブ／ゲーム アプリも開発できるようになりました。

　Xamrin は 本家 Visual Studio 2015 より標準でインストールできるようになり，Android のデザイナー，デバッガ，エミュレータまでも VS 2015 にはあります。<a href="http://www.idc.com/prodserv/smartphone-os-market-share.jsp" target="_blank">モバイル分野で後塵を拝した Microsoft</a> が本気になっているようです。

<a href="http://www.slideshare.net/chack411/net-core-5-windows-linux-os-x-docker" targe="_blanl">![VS2015.png](https://qiita-image-store.s3.amazonaws.com/0/67778/c472feec-9f9a-a24f-6fe5-8fa1d3d227f6.png)</a>

## .NET Core (Linux，Mac，Windows)

　近年の Web 開発は，JS を中心とした開発の生産性や性能の向上に挑戦し続けており，新しい技術が凄まじいスピードで誕生し続けています。特にフロントエンド技術の変遷は<a href="http://postd.cc/longevity-or-lack-thereof-in-javascript-frameworks" target="_blank">「JavaScriptフレームワークの寿命」</a>でも書かれたように，2012年より毎年フレームワークのトレンドが移り変わるという状態です。渦中の Web 技術おじさんとして，毎日楽しいのです。

　モダン Web 開発の例としては，バックエンドにイベント駆動型の Node.js，フロントエンドは仮想 DOM の React や双方向バインディングの AngularJS，データベースはスケールアウトを前提とした Cassandra や MongoDB，パッケージ管理に npm や Bower，ビルドシステムは gulp や Grunt，VSC (Version Control System) に Git を使い，CSS は PostCSS や Sass で書き stylelint で lint，CircleCI や Travis CI で継続的にビルドもする。

　対話形式でプロジェクトや画面のひな形を生成するスキャフォールディング（足場を組む）の Yeoman （ヨーマン）で MongoDB，Express，Angular，Node のスケルトン コードを生成させる，つまり Web 開発を JS でフルスタックに開発する <a href="http://meanjs.org/" target="_blank">"MEAN スタック"</a> が SPA (Single Page Application) の登場とともに有名になりました。

　このようなモダン Web 開発フローのひとつの選択肢として，バックエンドに C# も気軽に選択できるようにしたのが .NET Core をベースとする ASP.NET 5 が登場したひとつの背景だと思います。.NET Core はすべてオープン ソースで，クロス プラットフォームであり，Linux のサーバでも動作します。素晴らしいです。

<a href="http://blogs.msdn.com/b/bethmassi/archive/2015/02/25/understanding-net-2015.aspx" target="_blank">![NETCore.png](https://qiita-image-store.s3.amazonaws.com/0/67778/74360f5b-79d4-5c9c-0141-d85ffc5ba468.png)</a>

# VS Code (Linux, Mac, Windows)

　Visual Studio Code (VS Code) は Linux，Mac，Windows 上で無料で利用できる ~~<a href="https://atom.io/" target="_blank">Atom</a>~~ <a href="http://electron.atom.io/" target="_blank">Electron</a> を拡張したテキスト エディターです。テキスト エディターですが，実際に C# の開発に利用してみると，本家 IDE の Windows Visual Studio に匹敵する機能があり，本格的な C# 開発に耐えられるレベルの機能が備わっていると感じます。

　VS Code は導入も動作も軽量で素晴らしく，必要なモジュールのみをパッケージ管理 NuGet (ヌゲット，ニューゲット) で取得するスタイルは，**モダンな Web 開発のフローに慣れた Web 開発者にとても親しみやすく構成されている**と思います。

　逆に，従来の Windows Visual Studio が提供するリッチな GUI サポートで Web 開発に慣れた開発者にとっては，VS Code は 近年の移り変わりの激しい Web 開発技術の学習コストが高い割に，生産性が低く難解なもののように感じるのではないかと思われます。

　VS Code で実際に C# 開発を検証した結果をまとめました。

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
* ピークの定義を表示する
* ターミナルを開く
* Markdown のプレビュー
* Git
* 変更箇所の表示
* 変更を元に戻す
* パッケージ変更を検知によるパッケージの自動取得
* キー バインド カスタマイズ
* テーマ カスタマイズ
* <a href="https://code.visualstudio.com/Docs/editor/debugging" target="_blank">デバッグ (※Mono コンパイラーの場合) (Mono version 3.12 or later)</a>

## VS Code で出来なかったこと (一部)

* プロジェクト テンプレートの生成
* GUI のデザイナー (XAML，モデル等)
* デバッグ (※ASP.NET 5 Roslyn コンパイラーの場合) (VS Code でも将来可能となる予定)
* プロファイラー

## VS Code インストール

　<a href="https://www.visualstudio.com/ja-jp/products/code-vs.aspx" target="_blank">Visual Studio Code ベータ</a> より，Mac OS X 版をダウンロードしてください。 

# 環境構築

　Java の開発に JDK，Javascript の開発に node や npm が必要なように，Mac OS X の C# 開発には **DNX** (.NET Execution Environment) と **DNVM** (.NET Version Manager) が必要となります。

## DNX (.NET Execution Environment)

　DNX は *<a href="http://www.slideshare.net/chack411/net-core-5-windows-linux-os-x-docker" target="_blank">.NET アプリケーションをビルド & 実行するための SDK とランタイム</a>* です。

　DNX をインストールするには，DNX 自身をバージョン管理する DNVM が必要となります。DNVM と DNX のインストール<a href="https://docs.asp.net/en/latest/getting-started/installing-on-mac.html" target="_blank">手順</a>は以下のとおりです。

### Homebrew インストール

```bash
ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"
```
　ホームブリューは自家醸造 (自宅のガレージで PC を作っていたことから70年代 PC 製作 の隠語) を意味しますので，完了のアイコンがビールです。名前の由来は <a href="https://ja.wikipedia.org/wiki/%E3%83%9B%E3%83%BC%E3%83%A0%E3%83%96%E3%83%AA%E3%83%A5%E3%83%BC%E3%83%BB%E3%82%B3%E3%83%B3%E3%83%94%E3%83%A5%E3%83%BC%E3%82%BF%E3%83%BB%E3%82%AF%E3%83%A9%E3%83%96" target="_blank">Homebrew Computer Club</a> でしょう。たぶん。

### ICU4C インストール
 
```bash
brew install icu4c
```
　ICU4C (International Components for Unicode for C/C++ クラス・ライブラリー)。Java 版の ICU4J はテキスト ファイルの文字コード判定に使ったことがあります。テキストのサンプル数が少ないときにいかに文字コードを誤検知しないようにするか以下略。

### DNVM インストール

```bash
curl -sSL https://raw.githubusercontent.com/aspnet/Home/dev/dnvminstall.sh | DNX_BRANCH=dev sh && source ~/.dnx/dnvm/dnvm.sh
```
　curl コマンドを HTTP/2 化する方法を <a href="https://twitter.com/igrigorik/status/674675654399365120" target="_blank">イリヤ・グリゴリクがつぶやいて</a>いました。HTTP/2 は Wireshark でデコードもできるようです。tcpdump で キャプチャして Wireshark でデコードは面倒なので，tcpdump だけでデコードできないですかね。

### DNX インストール

```bash
dnvm upgrade -r coreclr
```
Core CLR (Common Language Runtime: 共通言語ランタイム) です。Java の JVM に相当します。CLR の仕様書は <a href="http://www.ecma-international.org/publications/standards/Ecma-335.htm" target="_blank">Ecma-335</a> です。

### Mono インストール

```bash
dnvm upgrade -r mono
```
　Mono の父 ミゲル・デ・イカザ は GNOME の作者でもあります。ですから Visual Studio になくて Mono にある機能に Gtk# による画面開発があります。

　以上で，環境構築が完了です。

# 開発

　Hello World といえば，コンソールに Hello World を出力することです。<a href="http://rosettacode.org/wiki/Category:Programming_Languages" target="_blank">Rosetta Code には 595 の 言語で Hello World</a> が刻まれています。

　Hello World をファイルに書き込んで，出力することが Hello World 1.5 とすると，Hello World 2.0 は Star Wars の監督を Web 出力することなのかもしれません。 

## Hello World 2.0 テスト プロジェクト構成

```bash
StarWars
├── Domain
│   └── StarWarsContext.cs
├── Migrations
├── Model
│   └── Director.cs
├── Test
│   └── StarWarsCrudTest.cs
└── project.json
```          

| ファイル／フォルダ    | 役割              | 説明             |
|:-------------------|:------------------|:----------------|
| StarWars           |プロジェクト フォルダ | 実行時のエントリポイント名となります |
| project.json       | NuGet 用パッケージ管理ファイル | npm の package.js に相当します。**VS Code は本ファイルを検知して OmniSharp (コード補完機能) を有効化します**         |
| StarWarsContext.cs | EF7 Npgsql 用 DB 接続情報 | Postgres の DB 接続情報を記述します。DbContext と呼びます |
| Director.cs        | EF7 用のテーブル／モデル | Postgres のテーブルに対応するモデルです |
| StarWarsCrudTest.cs | xUnit.net 用の自動テストコード | テストファイルです |


## NuGet パッケージ管理 

　<a href="https://msdn.microsoft.com/ja-jp/library/dn878908%28v=vs.110%29.aspx" targe="_blank">*.NET Core は，小規模のアセンブリ パッケージで NuGet を介してリリースされるためモジュール形式となっています。</a>*

　Hello World 2.0 で必要なパッケージは NuGet で入手します。json を書いてコマンドを実行すると，プロジェクトに必要なパッケージの依存関係を自動で解決してくれます。バージョンアップも 1 コマンドです。VS Code であれば package.json を変更して保存すると自動でパッケージの取得ダイアログを表示してくれるので便利です。


| パッケージ    | 役割              | 説明             |
|:-------------------|:------------------|:----------------|
| <a href="https://ef.readthedocs.org/en/latest/index.html" target="_blank">Entity Framework 7</a> | O/RM | CRUD やトランザクションの制御だけではなくテーブルの自動作成やマイグレーションも可能です | 
| <a href="http://www.npgsql.org/" target="_blank">Npgsql</a> | EF7 Postgres データ プロバイダー | EF7 で Postgres への接続を可能にします |
| <a href="http://xunit.github.io/docs/getting-started-dnx.html" target="_blank">xUnit.net</a> | 自動テストコード | OS X の ASP.NET 5 でも実行可能なユニット テスト フレームワークです |

### project.json

　NuGet によるパッケージ管理ファイルは以下のとおりです。

```json:project.json
{
    "dependencies": {
        "EntityFramework.Core": "7.0.0-*",
        "EntityFramework.Commands": "7.0.0-*",
        "EntityFramework.Relational": "7.0.0-*",
        "Microsoft.Extensions.DependencyInjection.Abstractions": "1.0.0-*",
        "Microsoft.Extensions.DependencyInjection": "1.0.0-*",
        "Npgsql": "3.1.0-*",
        "EntityFramework7.Npgsql": "3.1.0-*",
        "EntityFramework7.Npgsql.Design": "3.1.0-*",
        "xunit": "2.1.0-*",
        "xunit.runner.dnx": "2.1.0-*"
    },
    "commands": {
        "run": "StarWars",
        "ef": "EntityFramework.Commands",
        "test": "xunit.runner.dnx"
    },
    "frameworks": {
        "dnx451": {
            "frameworkAssemblies": {
                "System.Data": "4.0.0.0"
            }
        },
        "dnxcore50": {
            "dependencies": {
                "Microsoft.CSharp": "4.0.1-*",
                "System.Collections": "4.0.11-*",
                "System.Console": "4.0.0-*",
                "System.Linq": "4.0.1-*",
                "System.Threading": "4.0.11-*",
                "System.Data.Common": "4.0.1-*"
            }
        }
    }
}
```

　NuGet でパッケージを取得するコマンドは以下の通りです。

```bash
dnu restore
```

## Entity Framework 7

　DB のテーブルをプログラムでモデリングすると，モデルの定義に従って自動で DB のテーブルが生成され，しかもプログラムのモデルを変更すると DB のテーブルもマイグレーションすることが可能となるパッケージが Entity Framework の開発手法「コード ファースト」です。もちろん DB からモデルを生成する「データベース ファースト」開発にも EF7 は対応しています。 

### Model/Director.cs

　Postgres の テーブルに紐づく EF7 のモデルは，以下のとおりです。

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

　モデルにアトリビュート (Java のアノテーションに相当) の記述がないが大丈夫か？ と思ったのですが，大丈夫です。設定を最小限にする思想は EF が CoC (Convention over Configuration: 設定より規約) であるためです。規約に沿ったプログラムさえ書けば，面倒な設定はフレームワークがよしなに設定してくれます。本モデルでは テーブル名 + Id の DirectorId が自動的に主キーとして自動設定されます。

### Domain/StarWarsContext.cs

　EF7 から Postgres にアクセスするための接続情報 (DbContext) は以下の通りです。

```csharp:StarWarsContext.cs
using Microsoft.Data.Entity;
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

### EF7 マイグレーション

　DbContext とモデルからマイグレーションに必要なプログラムを自動生成するコマンドは以下のとおりです。

```bash
dnx ef migrations add StarWarsEp7
```

　上記コマンドで Migrations フォルダが作成され，Migrations フォルダの中に自動生成されたマイグレーション プログラムの断面が保存され続けます。

　Postgres にデータベースとテーブルを反映するコマンドは以下のとおりです。

```bash
dnx ef database update
```

![EF7MigrationsUpdate.png](https://qiita-image-store.s3.amazonaws.com/0/67778/b4127220-5c67-7350-8a2e-5885698a9877.png)

　モデリングしたとおりに starwars データベースと Director テーブルが作成されました。EF7 管理用の __EFMigrationsHistory も作成されています。

　update で実行される SQL を取得するコマンドは以下のとおりです。

```bash
dnx ef migrations script
```

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
dnx ef migrations add StarWarsEp7.2
dnx ef database update
```

![EF7MigrationsUpdate2.png](https://qiita-image-store.s3.amazonaws.com/0/67778/b0b7fe02-715d-fc58-3c7d-e8614ea1ea20.png)


　追加した項目 Born が追加されました。マイグレーションしたテーブルにすでにデータが存在する場合は，自動で初期値が設定されます。もちろんアトリビュートを使えば初期値，主キーや文字列長など細かく設定できます。

### Test/StarWarsCrudTest.cs

　xUnit.net を使って EF7 から Postgres に Insert するテストコードは以下のとおりです。

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
                db.Directors.Add(dirEp7);
                var count = db.SaveChanges();

                // assert
                Assert.Equal(1, count);
            }
        }
    }
}
```

　テストコードを実行するコマンドは以下のとおりです。

```bash
dnx test
```

　実行結果

```bash
k-kato:StarWars k_kato$ dnx test
xUnit.net DNX Runner (32-bit DNX 4.5.1)
  Discovering: StarWars
  Discovered:  StarWars
  Starting:    StarWars
  Finished:    StarWars
=== TEST EXECUTION SUMMARY ===
   StarWars  Total: 1, Errors: 0, Failed: 0, Skipped: 0, Time: 0.543s
```

![EF7Create.png](https://qiita-image-store.s3.amazonaws.com/0/67778/24be3cf9-3c95-f2a3-2b45-50fce22ca40b.png)



# まとめ
 
* Mac OS X 版 VS Code でも 本家 Windows VS に匹敵する C# 開発ができた
* NuGet を使用したパッケージ管理ができた
* EF7 のコード ファーストが実践できた
* EF7 で Postgres がマイグレーションできた
* EF7 で Postgres に C ~~RUD~~ できた
* xUnit.net で 自動テストコードが書けた
* ~~ASP.NET 5 で Star Wars ができた~~

　当初の目的を未達で時間を迎えてしまいました。無念です。


# 参考ノート

 1. Entity Framework Documentation, "Getting Started on OSX", http://ef.readthedocs.org/en/latest/getting-started/osx.html
 1. Npgsql, "CoreCLR (.NET Core)", http://www.npgsql.org/doc/coreclr.html
 1. xUnit.net Documentation, "Getting Started with xUnit.net (DNX / ASP.NET 5)", http://xunit.github.io/docs/getting-started-dnx.html
 1. Akira Inoue, 「.NET Core 5 ～ Windows, Linux, OS X そして Docker まで ～」, http://www.slideshare.net/chack411/net-core-5-windows-linux-os-x-docker
 1. 岩永信之，山田祥寛，井上章，伊藤伸裕，熊家賢治，神原淳史　著『C#エンジニア養成読本』技術評論社
 1. Visual Studio Code, "Working with C#", https://code.visualstudio.com/Docs/languages/csharp
 1. Visual Studio Code, "User and Workspace Settings", https://code.visualstudio.com/docs/customization/userandworkspace
 1. Visual Studio Code, "Debugging", https://code.visualstudio.com/docs/editor/debugging
 1. Visual Studio Code, "Editing Evolved", https://code.visualstudio.com/docs/editor/editingevolved
