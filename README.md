# HumeAI Unity package 

This is the branch that contains the Unity package layout for the HumeAI C# SDK.

If you are looking for the source code for the SDK, check out the `main` branch of this repository.

## Manually regenerating the package

### Prerequisites
- Node.js
- .NET SDK (v8+)
- git

### Steps
``` bash
# checkout the main branch into a folder called main
git worktree add ./main main

# update the unity package from the SDK in that branch
npx fern-api/make-unity-sdk --sln ./main/src/HumeApi.sln --target ./Packages/com.humeai.unity --package ./

# remove the worktree
git worktree remove ./main
```

You should now have:
- an updated `./Packages/com.humeai.unity` folder built with the latest SDK code.
- a ./com.humeai.unity-<version>.tgz` file - the UPM package 

