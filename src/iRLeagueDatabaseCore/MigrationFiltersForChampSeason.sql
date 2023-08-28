/* Migrate the filterOptionEntity used on ResultConfigs to use json column for Conditions 
 * instead of previous FilterConditionEntity
 */

USE TestDatabase;

START TRANSACTION;

-- Populate `Conditions` column with stored values and perform necessary value conversions
UPDATE FilterOptions AS f
	JOIN ResultConfigurations AS rc
		on rc.ResultConfigId=f.ResultFilterResultConfigId
	SET f.ChampSeasonId = rc.ChampSeasonId;

DROP TABLE TmpFilterConditions;
DROP FUNCTION splitStringToJsonArray;

ROLLBACK;