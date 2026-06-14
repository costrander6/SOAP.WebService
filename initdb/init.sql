CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260614020106_InitialCreate') THEN
    CREATE TABLE "Findings" (
        "Id" uuid NOT NULL,
        "ScanResultId" uuid NOT NULL,
        "Title" text NOT NULL,
        "Description" text NOT NULL,
        "File" text NOT NULL,
        "LineStart" bigint NOT NULL,
        "LineEnd" bigint NOT NULL,
        CONSTRAINT "PK_Findings" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260614020106_InitialCreate') THEN
    CREATE TABLE "ScanResults" (
        "Id" uuid NOT NULL,
        "WorkflowRunId" uuid NOT NULL,
        "Scanner" text NOT NULL,
        "Timestamp" timestamp with time zone NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        CONSTRAINT "PK_ScanResults" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260614020106_InitialCreate') THEN
    CREATE TABLE "WorkflowRuns" (
        "Id" uuid NOT NULL,
        "ApiKeyHash" text NOT NULL,
        "Repo" text NOT NULL,
        "Branch" text NOT NULL,
        "Commit" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        CONSTRAINT "PK_WorkflowRuns" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260614020106_InitialCreate') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260614020106_InitialCreate', '10.0.9');
    END IF;
END $EF$;
COMMIT;

