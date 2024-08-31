# Hollis 的 USTH 智慧学工服务晚点签到程序

&emsp;&emsp;本项目**仅供学习使用**，使用时**自负风险**。

&emsp;&emsp;针对**黑龙江科技大学**开发，其他学校请自行测试。

&emsp;&emsp;基于此前的易班校本化晚点签到程序 [YBNAS](https://github.com/bianyukun1213/YBNAS) 修改。

## 安装

&emsp;&emsp;本程序基于 .NET 8 框架，采用独立部署模式，因此无需安装框架即可运行。

&emsp;&emsp;对于 Windows，只提供 x86 版本；解压 `USTHNAS.<版本号>.win-x86.zip`，`USTHNAS.exe` 即是程序入口。

&emsp;&emsp;对于 GNU/Linux，只提供 x64 版本；解压 `USTHNAS.<版本号>.linux-x64.zip`，`USTHNAS` 即是程序入口。使用前需赋予执行权限。

&emsp;&emsp;对于其他平台与架构，请自行编译。

&emsp;&emsp;在运行程序之前需**参照下文编写配置文件**。

## 配置

&emsp;&emsp;默认配置文件 `config.json` 位于程序目录。

&emsp;&emsp;可使用命令行参数 `--config-path <PATH>` 或 `-c <PATH>` 指定替代的配置文件读取路径。还可使用 `--cache-path <PATH>` 或 `-k <PATH>` 指定替代的缓存文件读取路径；使用 `--log-path <PATH>` 或 `-l <PATH>` 指定替代的日志写入路径。

&emsp;&emsp;可配置多账号批量签到。

``` JSON
{
  "AutoSignIn": true,
  "AutoExit": false,
  "Execute": "",
  "Proxy": "",
  "Shuffle": true,
  "MaxRunningTasks": 4,
  "MaxRetries": 3,
  "RandomDelay": [ 1, 10 ],
  "ExpireIn": 0,
  "SignInConfigs": [
    {
      "Enable": true,
      "Force": false,
      "Name": "姓名或昵称，选填，留空则从接口获取",
      "Description": "注释，选填",
      "LoginAccount": "登录账号（学号）",
      "Password": "密码",
      "CollegeId": "1365853906458521620",
      "UserAgent": "Mozilla/5.0 (iPhone; CPU iPhone OS 17_6_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 MicroMessenger/8.0.50(0x1800323b) NetType/4G Language/zh_CN MiniProgramEnv/ios",
      "Position": [
        126.65892872841522,
        45.820275900282255
      ],
      "TimeSpan": [
        21,
        50,
        22,
        40
      ]
    },
    {
      "Enable": true,
      "Force": false,
      "Name": "李四",
      "Description": "寝室老六",
      "LoginAccount": "登录账号 2",
      "Password": "密码 2",
      "CollegeId": "1365853906458521620",
      "UserAgent": "Mozilla/5.0 (Linux; Android 12; ELS-AN00 Build/HUAWEIELS-AN00; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/126.0.6478.188 Mobile Safari/537.36 XWEB/1260117 MMWEBSDK/20240501 MMWEBID/265 MicroMessenger/8.0.50.2701(0x28003259) WeChat/arm64 Weixin NetType/5G Language/zh_CN ABI/arm64 MiniProgramEnv/android",
      "Position": [
        126.65892872841522,
        45.820275900282255
      ],
      "TimeSpan": [
        21,
        50,
        22,
        40
      ]
    }
  ]
}
```

&emsp;&emsp;`AutoSignIn` 字段配置是否自动签到。如不自动签到，则等待用户按任意键签到。

&emsp;&emsp;`AutoExit` 字段配置完成所有任务的执行或程序运行出错后，是否自动退出。如不自动退出，则等待用户按任意键退出。

&emsp;&emsp;`Execute` 字段配置完成所有任务的执行后运行的命令，如 `code \"{%RESULT_TEMP%}\"`，它会调用 Visual Studio Code 打开结果文件。可使用的变量有结果文件 `{%RESULT_TEMP%}`、总签到配置条数 `{%SIGN_IN_CONFIG_COUNT%}`、已解析的签到配置条数 `{%TASK_COUNT%}`、运行中的任务数量 `{%TASKS_RUNNING%}`、已完成的任务数量 `{%TASKS_COMPLETE%}`、已跳过的任务数量 `{%TASKS_SKIPPED%}`、等待中的任务数量 `{%TASKS_WAITING%}`、已终止的任务数量 `{%TASKS_ABORTED%}`。“运行中的任务数量”和“等待中的任务数量”基本上没什么用。

&emsp;&emsp;`Proxy` 字段配置网络代理，须以 `http://`、`https://`、`socks4://`、`socks4a://` 或 `socks5://` 开头。若为空字符串，则不使用代理。

&emsp;&emsp;`Shuffle` 字段配置是否打乱签到顺序。若不打乱，则按照配置文件内的顺序签到。

&emsp;&emsp;`MaxRunningTasks` 字段配置初始同时运行任务数，可以简单理解为最多同时运行几个任务，内置值及配置默认值均为 `4`。

&emsp;&emsp;`MaxRetries` 字段配置每个任务最大的重试次数，若该任务重试次数已到达该值，就不再重试。内置值及配置默认值均为 `3`。

&emsp;&emsp;`RandomDelay` 字段配置是否在获取签到信息和开始签到之间随机插入以秒为单位的延迟以使多账号批量签到的时间呈一定随机性，以及该随机延迟的范围。若两个值都是 `0`，则不延迟签到；若两个值相同且不是 `0`，则以该固定值作为延迟秒数，因此多账号批量签到的时间可能看起来很整齐；若两个值不同，则在该范围内取随机值作为延迟秒数。延迟最大可设 120s。

&emsp;&emsp;`ExpireIn` 字段配置以秒为单位的缓存有效期。以 `300` 为例：在程序重复运行时，对于每个签到账号，若上次签到成功的时刻距离此刻不超过 300s，则跳过此账号的签到。跨日期的情况不计算在内。内置值及默认值均为 `0`，即在程序重复运行时，对于每个签到账号，不论上次签到成功是何时，都重复签到。设为 `-1` 则总是跳过已经签到成功的账号。可删除 `cache` 文件重置缓存。

&emsp;&emsp;`SignInConfigs` 为签到任务配置，其中：

&emsp;&emsp;`Enable` 字段配置这条签到配置是否启用。未启用的签到配置在解析时跳过。

&emsp;&emsp;`Force` 字段配置是否强制签到。启用强制签到后，将跳过 `ExpireIn`、`TimeSpan` 字段检查与本机签到条件检查，直接向服务端发送签到请求。需要注意的是，强制签到并不代表“强制成功签到”——成功与否仍然取决于服务端的判断。在目前的情况下，启用强制签到后，如果其他参数有效，即使不在签到时间段内也能够签到成功，因为服务端并没有对签到时间作检查；但如果 `Position` 字段的配置不在签到范围内，即使启用了强制签到，签到仍然会失败。

&emsp;&emsp;`CollegeId` 字段如其名，配置学校 ID。黑龙江科技大学的 ID 固定是 `1365853906458521620`，不要改动。

&emsp;&emsp;`UserAgent` 字段配置用户代理字符串。目前智慧学工服务系统没有对其进行验证。模板中第一条对应 iPhone，第二条对应华为 P40 Pro，可任选使用、留空或填写任意内容。

&emsp;&emsp;`Position` 字段配置签到坐标，第一个值是经度，第二个值是纬度。默认坐标在第十八学生公寓。以下是学生公寓坐标的速查表，若无所需坐标，可以在[这个网页](https://lbs.amap.com/api/javascript-api/guide/services/geocoder)的“UI组件-拖拽选址”部分获取。

| 学生公寓 | 坐标 |
| --- | --- |
| 1 | `[126.654294, 45.818908]` |
| 2 | `[126.655473, 45.819054]` |
| 3 | `[126.655257, 45.818691]` |
| 4 | `[126.654784, 45.817655]` |
| 5 | `[126.65559, 45.818224]` |
| 6 | `[126.655823, 45.817834]` |
| 8 | `[126.656913, 45.819759]` |
| 9 | `[126.658324, 45.819912]` |
| 10 | `[126.657017, 45.819249]` |
| 11 | `[126.657777, 45.819467]` |
| 12 | `[126.657144, 45.818757]` |
| 13 | `[126.657911, 45.818985]` |
| 14 | `[126.657021, 45.818222]` |
| 15 | `[126.657755, 45.818443]` |
| 16 | `[126.658812, 45.819026]` |
| 17 | `[126.658587, 45.820794]` |
| 18 | `[126.659091, 45.820241]` |
| 19 | `[126.655363, 45.822561]` |
| 20 | `[126.65648, 45.822366]` |
| 21 | `[126.65708, 45.822313]` |
| 22 | `[126.657778, 45.822337]` |

&emsp;&emsp;`TimeSpan` 字段配置签到时间段，以系统时间为准，按顺序分别填开始小时、开始分钟、结束小时、结束分钟，且开始时间必须早于结束时间。程序运行时读取系统时间，若其不在此字段设定的时间段内，则跳过此用户的签到任务创建。另外，签到时也会动态获取当前时间并转换为北京时间，与智慧学工服务系统接口返回的签到时间段比对，若不在允许的时间段内，同样也会跳过签到。

## 使用的库

- [CommandLineParser](https://github.com/commandlineparser/commandline)
- [Flurl](https://github.com/tmenier/Flurl)
- [NLog](https://github.com/NLog/NLog)
