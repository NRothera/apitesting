Feature: GetPosts
	Test GET posts operation with restshar.net

@mytag
Scenario: VarifyAuthorOfThePosts
	Given I perfom GET operation for "posts/{postid}"
	And I perform operation for post "1"
	Then I should see the "author" name as "typicode"

@mytag
Scenario: VarifyAuthorOfThePosts 16
	Given I perfom GET operation for "posts/{postid}"
	And I perform operation for post "16"
	Then I should see the "author" name as "Execute Automation"

@mytag
Scenario: VarifyAuthorOfThePosts 18
	Given I perfom GET operation for "posts/{postid}"
	And I perform operation for post "18"
	Then I should see the "author" name as "Execute Automation"
