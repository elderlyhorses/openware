﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Alphabetize {
    public class AlphabetizeGameManager : MonoBehaviour {

        public TextMeshProUGUI CountdownText;
        public TextMeshProUGUI FeedbackText;
        public GridLayoutGroup ButtonGrid;
        public GameObject ButtonPrefab;
        public Color CorrectTextColor;
        public Color RegularTextColor;
        public int NumberOfWords = 3;
        public MinigameCompletionHandler MinigameCompletionHandler;

        List<string> wordList = new List<string>();
        List<string> spawnedWords = new List<string>();
        List<string> correctWords = new List<string>();
        Dictionary<string, TextMeshProUGUI> textUIForString = new Dictionary<string, TextMeshProUGUI>();

        bool isComplete = false;
        int secondsRemaining = 10;

        private void Awake() {
            populateWordList();

            FeedbackText.text = "";

            for (int x = 0; x < NumberOfWords; x++) {
                int ind = Random.Range(0, wordList.Count);
                string newWord = wordList[ind];
                wordList.RemoveAt(ind);
                spawnedWords.Add(newWord);

                GameObject newButton = Instantiate(ButtonPrefab);
                TextMeshProUGUI text = newButton.GetComponentInChildren<TextMeshProUGUI>();
                text.color = RegularTextColor;
                textUIForString[newWord] = text;
                text.text = newWord;
                newButton.transform.SetParent(ButtonGrid.transform);
            }

            CountdownText.text = "";
            spawnedWords.Sort();
            InvokeRepeating("countdown", 0f, 1f);
        }

        void countdown() {
            if (isComplete) {
                return;
            }

            CountdownText.text = "" + secondsRemaining;
            secondsRemaining--;

            if (secondsRemaining <= 0) {
                handleLose();
            }
        }

        public void DidTapWord(GameObject obj) {
            if (isComplete) {
                return;
            }

            TextMeshProUGUI text = obj.GetComponentInChildren<TextMeshProUGUI>();
            string word = text.text;
            
            if (correctWords.Contains(word)) {
                return;
            }

            if (word == spawnedWords[correctWords.Count]) {
                FeedbackText.text = "Correct, keep going!";
                correctWords.Add(word);
                text.color = CorrectTextColor;
            } else {
                FeedbackText.text = "Incorrect, start again!";

                foreach (string w in correctWords) {
                    textUIForString[w].color = RegularTextColor;
                }

                correctWords.Clear();
            }

            if (correctWords.Count == NumberOfWords) {
                handleWin();
            }
        }

        void handleLose() {
            if (isComplete) {
                return;
            }

            isComplete = true;
            FeedbackText.text = "You lose";
            MinigameCompletionHandler.LoseCallback.Invoke();
            StartCoroutine("loseCallback");
        }

        IEnumerator loseCallback() {
            yield return new WaitForSeconds(2);
            MinigameCompletionHandler.LoseCallback.Invoke();
        }

        void handleWin() {
            if (isComplete) {
                return;
            }

            isComplete = true;
            FeedbackText.text = "You win!";
            StartCoroutine("winCallback");
        }

        IEnumerator winCallback() {
            yield return new WaitForSeconds(2);
            MinigameCompletionHandler.WinCallback.Invoke();
        }

        void populateWordList() {
            wordList.Add("age");
            wordList.Add("am");
            wordList.Add("anger");
            wordList.Add("any");
            wordList.Add("art");
            wordList.Add("atom");
            wordList.Add("baby");
            wordList.Add("bad");
            wordList.Add("bar");
            wordList.Add("beat");
            wordList.Add("black");
            wordList.Add("blue");
            wordList.Add("bread");
            wordList.Add("camp");
            wordList.Add("cat");
            wordList.Add("cell");
            wordList.Add("class");
            wordList.Add("common");
            wordList.Add("copy");
            wordList.Add("cotton");
            wordList.Add("count");
            wordList.Add("cow");
            wordList.Add("crop");
            wordList.Add("dance");
            wordList.Add("day");
            wordList.Add("die");
            wordList.Add("dog");
            wordList.Add("door");
            wordList.Add("dress");
            wordList.Add("drive");
            wordList.Add("duck");
            wordList.Add("east");
            wordList.Add("end");
            wordList.Add("energy");
            wordList.Add("far");
            wordList.Add("fat");
            wordList.Add("fire");
            wordList.Add("floor");
            wordList.Add("found");
            wordList.Add("four");
            wordList.Add("free");
            wordList.Add("gas");
            wordList.Add("gentle");
            wordList.Add("go");
            wordList.Add("gray");
            wordList.Add("held");
            wordList.Add("hit");
            wordList.Add("hope");
            wordList.Add("hundred");
            wordList.Add("ice");
            wordList.Add("idea");
            wordList.Add("if");
            wordList.Add("join");
            wordList.Add("just");
            wordList.Add("lay");
            wordList.Add("leg");
            wordList.Add("level");
            wordList.Add("lift");
            wordList.Add("locate");
            wordList.Add("loud");
            wordList.Add("match");
            wordList.Add("me");
            wordList.Add("my");
            wordList.Add("need");
            wordList.Add("next");
            wordList.Add("oh");
            wordList.Add("old");
            wordList.Add("open");
            wordList.Add("page");
            wordList.Add("pair");
            wordList.Add("parent");
            wordList.Add("poor");
            wordList.Add("push");
            wordList.Add("rail");
            wordList.Add("rich");
            wordList.Add("save");
            wordList.Add("say");
            wordList.Add("seat");
            wordList.Add("sell");
            wordList.Add("shop");
            wordList.Add("sight");
            wordList.Add("sister");
            wordList.Add("so");
            wordList.Add("soil");
            wordList.Add("solve");
            wordList.Add("speak");
            wordList.Add("street");
            wordList.Add("swim");
            wordList.Add("ten");
            wordList.Add("throw");
            wordList.Add("thus");
            wordList.Add("trip");
            wordList.Add("try");
            wordList.Add("tube");
            wordList.Add("two");
            wordList.Add("type");
            wordList.Add("west");
            wordList.Add("who");
            wordList.Add("why");
            wordList.Add("wide");
            wordList.Add("wife");
            wordList.Add("win");
            wordList.Add("wing");
            wordList.Add("women");
            wordList.Add("wonder");
            wordList.Add("a");
            wordList.Add("able");
            wordList.Add("about");
            wordList.Add("above");
            wordList.Add("act");
            wordList.Add("Add");
            wordList.Add("afraid");
            wordList.Add("after");
            wordList.Add("again");
            wordList.Add("against");
            wordList.Add("ago");
            wordList.Add("agree");
            wordList.Add("air");
            wordList.Add("all");
            wordList.Add("allow");
            wordList.Add("also");
            wordList.Add("always");
            wordList.Add("among");
            wordList.Add("an");
            wordList.Add("and");
            wordList.Add("animal");
            wordList.Add("answer");
            wordList.Add("appear");
            wordList.Add("apple");
            wordList.Add("are");
            wordList.Add("area");
            wordList.Add("arm");
            wordList.Add("arrange");
            wordList.Add("arrive");
            wordList.Add("as");
            wordList.Add("ask");
            wordList.Add("at");
            wordList.Add("back");
            wordList.Add("ball");
            wordList.Add("band");
            wordList.Add("bank");
            wordList.Add("base");
            wordList.Add("basic");
            wordList.Add("bat");
            wordList.Add("be");
            wordList.Add("bear");
            wordList.Add("beauty");
            wordList.Add("bed");
            wordList.Add("been");
            wordList.Add("before");
            wordList.Add("began");
            wordList.Add("begin");
            wordList.Add("behind");
            wordList.Add("believe");
            wordList.Add("bell");
            wordList.Add("best");
            wordList.Add("better");
            wordList.Add("between");
            wordList.Add("big");
            wordList.Add("bird");
            wordList.Add("bit");
            wordList.Add("block");
            wordList.Add("blood");
            wordList.Add("blow");
            wordList.Add("board");
            wordList.Add("boat");
            wordList.Add("body");
            wordList.Add("bone");
            wordList.Add("book");
            wordList.Add("born");
            wordList.Add("both");
            wordList.Add("bottom");
            wordList.Add("bought");
            wordList.Add("box");
            wordList.Add("boy");
            wordList.Add("branch");
            wordList.Add("break");
            wordList.Add("bright");
            wordList.Add("bring");
            wordList.Add("broad");
            wordList.Add("broke");
            wordList.Add("brother");
            wordList.Add("brought");
            wordList.Add("brown");
            wordList.Add("build");
            wordList.Add("burn");
            wordList.Add("busy");
            wordList.Add("but");
            wordList.Add("buy");
            wordList.Add("by");
            wordList.Add("call");
            wordList.Add("came");
            wordList.Add("can");
            wordList.Add("capital");
            wordList.Add("captain");
            wordList.Add("car");
            wordList.Add("card");
            wordList.Add("care");
            wordList.Add("carry");
            wordList.Add("case");
            wordList.Add("catch");
            wordList.Add("caught");
            wordList.Add("cause");
            wordList.Add("cent");
            wordList.Add("center");
            wordList.Add("century");
            wordList.Add("certain");
            wordList.Add("chair");
            wordList.Add("chance");
            wordList.Add("change");
            wordList.Add("character");
            wordList.Add("charge");
            wordList.Add("chart");
            wordList.Add("check");
            wordList.Add("chick");
            wordList.Add("chief");
            wordList.Add("child");
            wordList.Add("children");
            wordList.Add("choose");
            wordList.Add("chord");
            wordList.Add("circle");
            wordList.Add("city");
            wordList.Add("claim");
            wordList.Add("clean");
            wordList.Add("clear");
            wordList.Add("climb");
            wordList.Add("clock");
            wordList.Add("close");
            wordList.Add("clothe");
            wordList.Add("cloud");
            wordList.Add("coast");
            wordList.Add("coat");
            wordList.Add("cold");
            wordList.Add("collect");
            wordList.Add("colony");
            wordList.Add("color");
            wordList.Add("column");
            wordList.Add("come");
            wordList.Add("company");
            wordList.Add("compare");
            wordList.Add("complete");
            wordList.Add("condition");
            wordList.Add("connect");
            wordList.Add("consider");
            wordList.Add("consonant");
            wordList.Add("contain");
            wordList.Add("continent");
            wordList.Add("continue");
            wordList.Add("control");
            wordList.Add("cook");
            wordList.Add("cool");
            wordList.Add("corn");
            wordList.Add("corner");
            wordList.Add("correct");
            wordList.Add("cost");
            wordList.Add("could");
            wordList.Add("country");
            wordList.Add("course");
            wordList.Add("cover");
            wordList.Add("crease");
            wordList.Add("create");
            wordList.Add("cross");
            wordList.Add("crowd");
            wordList.Add("cry");
            wordList.Add("current");
            wordList.Add("cut");
            wordList.Add("dad");
            wordList.Add("danger");
            wordList.Add("dark");
            wordList.Add("dead");
            wordList.Add("deal");
            wordList.Add("dear");
            wordList.Add("death");
            wordList.Add("decide");
            wordList.Add("decimal");
            wordList.Add("deep");
            wordList.Add("degree");
            wordList.Add("depend");
            wordList.Add("describe");
            wordList.Add("desert");
            wordList.Add("design");
            wordList.Add("determine");
            wordList.Add("develop");
            wordList.Add("dictionary");
            wordList.Add("did");
            wordList.Add("differ");
            wordList.Add("difficult");
            wordList.Add("direct");
            wordList.Add("discuss");
            wordList.Add("distant");
            wordList.Add("divide");
            wordList.Add("division");
            wordList.Add("do");
            wordList.Add("doctor");
            wordList.Add("does");
            wordList.Add("dollar");
            wordList.Add("don't");
            wordList.Add("done");
            wordList.Add("double");
            wordList.Add("down");
            wordList.Add("draw");
            wordList.Add("dream");
            wordList.Add("drink");
            wordList.Add("drop");
            wordList.Add("dry");
            wordList.Add("during");
            wordList.Add("each");
            wordList.Add("ear");
            wordList.Add("early");
            wordList.Add("earth");
            wordList.Add("ease");
            wordList.Add("eat");
            wordList.Add("edge");
            wordList.Add("effect");
            wordList.Add("egg");
            wordList.Add("eight");
            wordList.Add("either");
            wordList.Add("electric");
            wordList.Add("element");
            wordList.Add("else");
            wordList.Add("enemy");
            wordList.Add("engine");
            wordList.Add("enough");
            wordList.Add("enter");
            wordList.Add("equal");
            wordList.Add("equate");
            wordList.Add("especially");
            wordList.Add("even");
            wordList.Add("evening");
            wordList.Add("event");
            wordList.Add("ever");
            wordList.Add("every");
            wordList.Add("exact");
            wordList.Add("example");
            wordList.Add("except");
            wordList.Add("excite");
            wordList.Add("exercise");
            wordList.Add("expect");
            wordList.Add("experience");
            wordList.Add("experiment");
            wordList.Add("eye");
            wordList.Add("face");
            wordList.Add("fact");
            wordList.Add("fair");
            wordList.Add("fall");
            wordList.Add("family");
            wordList.Add("famous");
            wordList.Add("farm");
            wordList.Add("fast");
            wordList.Add("father");
            wordList.Add("favor");
            wordList.Add("fear");
            wordList.Add("feed");
            wordList.Add("feel");
            wordList.Add("feet");
            wordList.Add("fell");
            wordList.Add("felt");
            wordList.Add("few");
            wordList.Add("field");
            wordList.Add("fig");
            wordList.Add("fight");
            wordList.Add("figure");
            wordList.Add("fill");
            wordList.Add("final");
            wordList.Add("find");
            wordList.Add("fine");
            wordList.Add("finger");
            wordList.Add("finish");
            wordList.Add("first");
            wordList.Add("fish");
            wordList.Add("fit");
            wordList.Add("five");
            wordList.Add("flat");
            wordList.Add("flow");
            wordList.Add("flower");
            wordList.Add("fly");
            wordList.Add("follow");
            wordList.Add("food");
            wordList.Add("foot");
            wordList.Add("for");
            wordList.Add("force");
            wordList.Add("forest");
            wordList.Add("form");
            wordList.Add("forward");
            wordList.Add("fraction");
            wordList.Add("fresh");
            wordList.Add("friend");
            wordList.Add("from");
            wordList.Add("front");
            wordList.Add("fruit");
            wordList.Add("full");
            wordList.Add("fun");
            wordList.Add("game");
            wordList.Add("garden");
            wordList.Add("gather");
            wordList.Add("gave");
            wordList.Add("general");
            wordList.Add("get");
            wordList.Add("girl");
            wordList.Add("give");
            wordList.Add("glad");
            wordList.Add("glass");
            wordList.Add("gold");
            wordList.Add("gone");
            wordList.Add("good");
            wordList.Add("got");
            wordList.Add("govern");
            wordList.Add("grand");
            wordList.Add("grass");
            wordList.Add("great");
            wordList.Add("green");
            wordList.Add("grew");
            wordList.Add("ground");
            wordList.Add("group");
            wordList.Add("grow");
            wordList.Add("guess");
            wordList.Add("guide");
            wordList.Add("gun");
            wordList.Add("had");
            wordList.Add("hair");
            wordList.Add("half");
            wordList.Add("hand");
            wordList.Add("happen");
            wordList.Add("happy");
            wordList.Add("hard");
            wordList.Add("has");
            wordList.Add("hat");
            wordList.Add("have");
            wordList.Add("he");
            wordList.Add("head");
            wordList.Add("hear");
            wordList.Add("heard");
            wordList.Add("heart");
            wordList.Add("heat");
            wordList.Add("heavy");
            wordList.Add("help");
            wordList.Add("her");
            wordList.Add("here");
            wordList.Add("high");
            wordList.Add("hill");
            wordList.Add("him");
            wordList.Add("his");
            wordList.Add("history");
            wordList.Add("hold");
            wordList.Add("hole");
            wordList.Add("home");
            wordList.Add("horse");
            wordList.Add("hot");
            wordList.Add("hour");
            wordList.Add("house");
            wordList.Add("how");
            wordList.Add("huge");
            wordList.Add("human");
            wordList.Add("hunt");
            wordList.Add("hurry");
            wordList.Add("I");
            wordList.Add("imagine");
            wordList.Add("inch");
            wordList.Add("include");
            wordList.Add("indicate");
            wordList.Add("industry");
            wordList.Add("insect");
            wordList.Add("instant");
            wordList.Add("instrument");
            wordList.Add("interest");
            wordList.Add("invent");
            wordList.Add("iron");
            wordList.Add("is");
            wordList.Add("island");
            wordList.Add("it");
            wordList.Add("job");
            wordList.Add("joy");
            wordList.Add("jump");
            wordList.Add("keep");
            wordList.Add("kept");
            wordList.Add("key");
            wordList.Add("kill");
            wordList.Add("kind");
            wordList.Add("king");
            wordList.Add("knew");
            wordList.Add("know");
            wordList.Add("lady");
            wordList.Add("lake");
            wordList.Add("land");
            wordList.Add("language");
            wordList.Add("large");
            wordList.Add("last");
            wordList.Add("late");
            wordList.Add("laugh");
            wordList.Add("law");
            wordList.Add("lead");
            wordList.Add("learn");
            wordList.Add("least");
            wordList.Add("leave");
            wordList.Add("led");
            wordList.Add("left");
            wordList.Add("length");
            wordList.Add("less");
            wordList.Add("let");
            wordList.Add("letter");
            wordList.Add("lie");
            wordList.Add("life");
            wordList.Add("light");
            wordList.Add("like");
            wordList.Add("line");
            wordList.Add("liquid");
            wordList.Add("list");
            wordList.Add("listen");
            wordList.Add("little");
            wordList.Add("live");
            wordList.Add("log");
            wordList.Add("lone");
            wordList.Add("long");
            wordList.Add("look");
            wordList.Add("lost");
            wordList.Add("lot");
            wordList.Add("love");
            wordList.Add("low");
            wordList.Add("machine");
            wordList.Add("made");
            wordList.Add("magnet");
            wordList.Add("main");
            wordList.Add("major");
            wordList.Add("make");
            wordList.Add("man");
            wordList.Add("many");
            wordList.Add("map");
            wordList.Add("mark");
            wordList.Add("market");
            wordList.Add("mass");
            wordList.Add("master");
            wordList.Add("material");
            wordList.Add("matter");
            wordList.Add("may");
            wordList.Add("mean");
            wordList.Add("meant");
            wordList.Add("measure");
            wordList.Add("meat");
            wordList.Add("meet");
            wordList.Add("melody");
            wordList.Add("men");
            wordList.Add("metal");
            wordList.Add("method");
            wordList.Add("middle");
            wordList.Add("might");
            wordList.Add("mile");
            wordList.Add("milk");
            wordList.Add("million");
            wordList.Add("mind");
            wordList.Add("mine");
            wordList.Add("minute");
            wordList.Add("miss");
            wordList.Add("mix");
            wordList.Add("modern");
            wordList.Add("molecule");
            wordList.Add("moment");
            wordList.Add("money");
            wordList.Add("month");
            wordList.Add("moon");
            wordList.Add("more");
            wordList.Add("morning");
            wordList.Add("most");
            wordList.Add("mother");
            wordList.Add("motion");
            wordList.Add("mount");
            wordList.Add("mountain");
            wordList.Add("mouth");
            wordList.Add("move");
            wordList.Add("much");
            wordList.Add("multiply");
            wordList.Add("music");
            wordList.Add("must");
            wordList.Add("name");
            wordList.Add("nation");
            wordList.Add("natural");
            wordList.Add("nature");
            wordList.Add("near");
            wordList.Add("necessary");
            wordList.Add("neck");
            wordList.Add("neighbor");
            wordList.Add("never");
            wordList.Add("new");
            wordList.Add("night");
            wordList.Add("nine");
            wordList.Add("no");
            wordList.Add("noise");
            wordList.Add("noon");
            wordList.Add("nor");
            wordList.Add("north");
            wordList.Add("nose");
            wordList.Add("not");
            wordList.Add("note");
            wordList.Add("nothing");
            wordList.Add("notice");
            wordList.Add("noun");
            wordList.Add("now");
            wordList.Add("number");
            wordList.Add("numeral");
            wordList.Add("object");
            wordList.Add("observe");
            wordList.Add("occur");
            wordList.Add("ocean");
            wordList.Add("of");
            wordList.Add("off");
            wordList.Add("offer");
            wordList.Add("office");
            wordList.Add("often");
            wordList.Add("oil");
            wordList.Add("on");
            wordList.Add("once");
            wordList.Add("one");
            wordList.Add("only");
            wordList.Add("operate");
            wordList.Add("opposite");
            wordList.Add("or");
            wordList.Add("order");
            wordList.Add("organ");
            wordList.Add("original");
            wordList.Add("other");
            wordList.Add("our");
            wordList.Add("out");
            wordList.Add("over");
            wordList.Add("own");
            wordList.Add("oxygen");
            wordList.Add("paint");
            wordList.Add("paper");
            wordList.Add("paragraph");
            wordList.Add("part");
            wordList.Add("particular");
            wordList.Add("party");
            wordList.Add("pass");
            wordList.Add("past");
            wordList.Add("path");
            wordList.Add("pattern");
            wordList.Add("pay");
            wordList.Add("people");
            wordList.Add("perhaps");
            wordList.Add("period");
            wordList.Add("person");
            wordList.Add("phrase");
            wordList.Add("pick");
            wordList.Add("picture");
            wordList.Add("piece");
            wordList.Add("pitch");
            wordList.Add("place");
            wordList.Add("plain");
            wordList.Add("plan");
            wordList.Add("plane");
            wordList.Add("planet");
            wordList.Add("plant");
            wordList.Add("play");
            wordList.Add("please");
            wordList.Add("plural");
            wordList.Add("poem");
            wordList.Add("point");
            wordList.Add("populate");
            wordList.Add("port");
            wordList.Add("pose");
            wordList.Add("position");
            wordList.Add("possible");
            wordList.Add("post");
            wordList.Add("pound");
            wordList.Add("power");
            wordList.Add("practice");
            wordList.Add("prepare");
            wordList.Add("present");
            wordList.Add("press");
            wordList.Add("pretty");
            wordList.Add("print");
            wordList.Add("probable");
            wordList.Add("problem");
            wordList.Add("process");
            wordList.Add("produce");
            wordList.Add("product");
            wordList.Add("proper");
            wordList.Add("property");
            wordList.Add("protect");
            wordList.Add("prove");
            wordList.Add("provide");
            wordList.Add("pull");
            wordList.Add("put");
            wordList.Add("quart");
            wordList.Add("question");
            wordList.Add("quick");
            wordList.Add("quiet");
            wordList.Add("quite");
            wordList.Add("quotient");
            wordList.Add("race");
            wordList.Add("radio");
            wordList.Add("rain");
            wordList.Add("raise");
            wordList.Add("ran");
            wordList.Add("range");
            wordList.Add("rather");
            wordList.Add("reach");
            wordList.Add("read");
            wordList.Add("ready");
            wordList.Add("real");
            wordList.Add("reason");
            wordList.Add("receive");
            wordList.Add("record");
            wordList.Add("red");
            wordList.Add("region");
            wordList.Add("remember");
            wordList.Add("repeat");
            wordList.Add("reply");
            wordList.Add("represent");
            wordList.Add("require");
            wordList.Add("rest");
            wordList.Add("result");
            wordList.Add("ride");
            wordList.Add("right");
            wordList.Add("ring");
            wordList.Add("rise");
            wordList.Add("river");
            wordList.Add("road");
            wordList.Add("rock");
            wordList.Add("roll");
            wordList.Add("room");
            wordList.Add("root");
            wordList.Add("rope");
            wordList.Add("rose");
            wordList.Add("round");
            wordList.Add("row");
            wordList.Add("rub");
            wordList.Add("rule");
            wordList.Add("run");
            wordList.Add("safe");
            wordList.Add("said");
            wordList.Add("sail");
            wordList.Add("salt");
            wordList.Add("same");
            wordList.Add("sand");
            wordList.Add("sat");
            wordList.Add("saw");
            wordList.Add("scale");
            wordList.Add("school");
            wordList.Add("science");
            wordList.Add("score");
            wordList.Add("sea");
            wordList.Add("search");
            wordList.Add("season");
            wordList.Add("second");
            wordList.Add("section");
            wordList.Add("see");
            wordList.Add("seed");
            wordList.Add("seem");
            wordList.Add("segment");
            wordList.Add("select");
            wordList.Add("self");
            wordList.Add("send");
            wordList.Add("sense");
            wordList.Add("sent");
            wordList.Add("sentence");
            wordList.Add("separate");
            wordList.Add("serve");
            wordList.Add("set");
            wordList.Add("settle");
            wordList.Add("seven");
            wordList.Add("several");
            wordList.Add("shall");
            wordList.Add("shape");
            wordList.Add("share");
            wordList.Add("sharp");
            wordList.Add("she");
            wordList.Add("sheet");
            wordList.Add("shell");
            wordList.Add("shine");
            wordList.Add("ship");
            wordList.Add("shoe");
            wordList.Add("shore");
            wordList.Add("short");
            wordList.Add("should");
            wordList.Add("shoulder");
            wordList.Add("shout");
            wordList.Add("show");
            wordList.Add("side");
            wordList.Add("sign");
            wordList.Add("silent");
            wordList.Add("silver");
            wordList.Add("similar");
            wordList.Add("simple");
            wordList.Add("since");
            wordList.Add("sing");
            wordList.Add("single");
            wordList.Add("sit");
            wordList.Add("six");
            wordList.Add("size");
            wordList.Add("skill");
            wordList.Add("skin");
            wordList.Add("sky");
            wordList.Add("slave");
            wordList.Add("sleep");
            wordList.Add("slip");
            wordList.Add("slow");
            wordList.Add("small");
            wordList.Add("smell");
            wordList.Add("smile");
            wordList.Add("snow");
            wordList.Add("soft");
            wordList.Add("soldier");
            wordList.Add("solution");
            wordList.Add("some");
            wordList.Add("son");
            wordList.Add("song");
            wordList.Add("soon");
            wordList.Add("sound");
            wordList.Add("south");
            wordList.Add("space");
            wordList.Add("special");
            wordList.Add("speech");
            wordList.Add("speed");
            wordList.Add("spell");
            wordList.Add("spend");
            wordList.Add("spoke");
            wordList.Add("spot");
            wordList.Add("spread");
            wordList.Add("spring");
            wordList.Add("square");
            wordList.Add("stand");
            wordList.Add("star");
            wordList.Add("start");
            wordList.Add("state");
            wordList.Add("station");
            wordList.Add("stay");
            wordList.Add("stead");
            wordList.Add("steam");
            wordList.Add("steel");
            wordList.Add("step");
            wordList.Add("stick");
            wordList.Add("still");
            wordList.Add("stone");
            wordList.Add("stood");
            wordList.Add("stop");
            wordList.Add("store");
            wordList.Add("story");
            wordList.Add("straight");
            wordList.Add("strange");
            wordList.Add("stream");
            wordList.Add("stretch");
            wordList.Add("string");
            wordList.Add("strong");
            wordList.Add("student");
            wordList.Add("study");
            wordList.Add("subject");
            wordList.Add("substance");
            wordList.Add("subtract");
            wordList.Add("success");
            wordList.Add("such");
            wordList.Add("sudden");
            wordList.Add("suffix");
            wordList.Add("sugar");
            wordList.Add("suggest");
            wordList.Add("suit");
            wordList.Add("summer");
            wordList.Add("sun");
            wordList.Add("supply");
            wordList.Add("support");
            wordList.Add("sure");
            wordList.Add("surface");
            wordList.Add("surprise");
            wordList.Add("syllable");
            wordList.Add("symbol");
            wordList.Add("system");
            wordList.Add("table");
            wordList.Add("tail");
            wordList.Add("take");
            wordList.Add("talk");
            wordList.Add("tall");
            wordList.Add("teach");
            wordList.Add("team");
            wordList.Add("teeth");
            wordList.Add("tell");
            wordList.Add("temperature");
            wordList.Add("term");
            wordList.Add("test");
            wordList.Add("than");
            wordList.Add("thank");
            wordList.Add("that");
            wordList.Add("the");
            wordList.Add("their");
            wordList.Add("them");
            wordList.Add("then");
            wordList.Add("there");
            wordList.Add("these");
            wordList.Add("they");
            wordList.Add("thick");
            wordList.Add("thin");
            wordList.Add("thing");
            wordList.Add("think");
            wordList.Add("third");
            wordList.Add("this");
            wordList.Add("those");
            wordList.Add("though");
            wordList.Add("thought");
            wordList.Add("thousand");
            wordList.Add("three");
            wordList.Add("through");
            wordList.Add("tie");
            wordList.Add("time");
            wordList.Add("tiny");
            wordList.Add("tire");
            wordList.Add("to");
            wordList.Add("together");
            wordList.Add("told");
            wordList.Add("tone");
            wordList.Add("too");
            wordList.Add("took");
            wordList.Add("tool");
            wordList.Add("top");
            wordList.Add("total");
            wordList.Add("touch");
            wordList.Add("toward");
            wordList.Add("town");
            wordList.Add("track");
            wordList.Add("trade");
            wordList.Add("train");
            wordList.Add("travel");
            wordList.Add("tree");
            wordList.Add("triangle");
            wordList.Add("trouble");
            wordList.Add("truck");
            wordList.Add("turn");
            wordList.Add("twenty");
            wordList.Add("under");
            wordList.Add("unit");
            wordList.Add("until");
            wordList.Add("up");
            wordList.Add("us");
            wordList.Add("use");
            wordList.Add("usual");
            wordList.Add("valley");
            wordList.Add("value");
            wordList.Add("vary");
            wordList.Add("verb");
            wordList.Add("very");
            wordList.Add("view");
            wordList.Add("village");
            wordList.Add("visit");
            wordList.Add("voice");
            wordList.Add("vowel");
            wordList.Add("wait");
            wordList.Add("walk");
            wordList.Add("wall");
            wordList.Add("want");
            wordList.Add("war");
            wordList.Add("warm");
            wordList.Add("was");
            wordList.Add("wash");
            wordList.Add("watch");
            wordList.Add("water");
            wordList.Add("wave");
            wordList.Add("way");
            wordList.Add("we");
            wordList.Add("wear");
            wordList.Add("weather");
            wordList.Add("week");
            wordList.Add("weight");
            wordList.Add("well");
            wordList.Add("went");
            wordList.Add("were");
            wordList.Add("what");
            wordList.Add("wheel");
            wordList.Add("when");
            wordList.Add("where");
            wordList.Add("whether");
            wordList.Add("which");
            wordList.Add("while");
            wordList.Add("white");
            wordList.Add("whole");
            wordList.Add("whose");
            wordList.Add("wild");
            wordList.Add("will");
            wordList.Add("wind");
            wordList.Add("window");
            wordList.Add("winter");
            wordList.Add("wire");
            wordList.Add("wish");
            wordList.Add("with");
            wordList.Add("woman");
            wordList.Add("won't");
            wordList.Add("wood");
            wordList.Add("word");
            wordList.Add("work");
            wordList.Add("world");
            wordList.Add("would");
            wordList.Add("write");
            wordList.Add("wrong");
            wordList.Add("wrote");
            wordList.Add("yard");
            wordList.Add("year");
            wordList.Add("yellow");
            wordList.Add("yes");
            wordList.Add("yet");
            wordList.Add("you");
            wordList.Add("young");
            wordList.Add("your");

        }
    }
}