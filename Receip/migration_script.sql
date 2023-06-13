IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230613140530_NewInitial')
BEGIN
    CREATE TABLE [Recipes] (
        [RecipeId] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Picture] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Recipes] PRIMARY KEY ([RecipeId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230613140530_NewInitial')
BEGIN
    CREATE TABLE [Ingredient] (
        [Id] int NOT NULL,
        [RecipesRecipeId] int NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Ingredient] PRIMARY KEY ([RecipesRecipeId], [Id]),
        CONSTRAINT [FK_Ingredient_Recipes_RecipesRecipeId] FOREIGN KEY ([RecipesRecipeId]) REFERENCES [Recipes] ([RecipeId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230613140530_NewInitial')
BEGIN
    CREATE TABLE [RecipeSteps] (
        [RecipeId] int NOT NULL,
        [StepNumber] int NOT NULL,
        [Step] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_RecipeSteps] PRIMARY KEY ([RecipeId], [StepNumber]),
        CONSTRAINT [FK_RecipeSteps_Recipes_RecipeId] FOREIGN KEY ([RecipeId]) REFERENCES [Recipes] ([RecipeId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230613140530_NewInitial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230613140530_NewInitial', N'6.0.15');
END;
GO

COMMIT;
GO

