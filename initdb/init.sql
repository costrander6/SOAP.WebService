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

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260615223204_AddApiKeyAssociations') THEN
    ALTER TABLE "WorkflowRuns" RENAME COLUMN "ApiKeyHash" TO "Owner";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260615223204_AddApiKeyAssociations') THEN
    CREATE TABLE "ApiKeyAssociations" (
        "Id" uuid NOT NULL,
        "KeyHash" text NOT NULL,
        "Owner" text NOT NULL,
        "CreatedAt" timestamp with time zone NOT NULL,
        "RevokedAt" timestamp with time zone,
        CONSTRAINT "PK_ApiKeyAssociations" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260615223204_AddApiKeyAssociations') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260615223204_AddApiKeyAssociations', '10.0.9');
    END IF;
END $EF$;
COMMIT;

START TRANSACTION;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260620005724_AddSeverity') THEN
    ALTER TABLE "ScanResults" DROP COLUMN "Timestamp";
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260620005724_AddSeverity') THEN
    ALTER TABLE "WorkflowRuns" ADD "Timestamp" timestamp with time zone NOT NULL DEFAULT TIMESTAMPTZ '-infinity';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260620005724_AddSeverity') THEN
    ALTER TABLE "Findings" ADD "Severity" text NOT NULL DEFAULT '';
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20260620005724_AddSeverity') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20260620005724_AddSeverity', '10.0.9');
    END IF;
END $EF$;
COMMIT;

