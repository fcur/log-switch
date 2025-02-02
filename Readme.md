# Serilog log level switch minimal app

The following configuration allows us to add a new switch for general logging and set the initial logging level to `Debug`. Switch name must include the leading `$` character.

```json
{
    "Serilog": {
        "MinimumLevel": {
            "ControlledBy": "$controlSwitch"
        },
        "LevelSwitches": {
            "controlSwitch": "Debug"
        }
    }
}
```


