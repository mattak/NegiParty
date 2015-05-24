#!/usr/bin/env ruby

PROJECT_DIR          = 'Exports/iOS/'
SCHEME               = 'Unity-iPhone'
PROJECT              = 'Unity-iPhone.xcodeproj'
CRASHLYTICS_SUBMIT   = 'Pods/Fabric/Crashlytics.framework/submit'
FABRIC_EMAIL         = ENV['FABRIC_EMAIL']
FABRIC_GROUP         = ENV['FABRIC_GROUP']
FABRIC_API_KEY       = ENV['FABRIC_API_KEY']
FABRIC_SECRET_KEY    = ENV['FABRIC_SECRET_KEY']
ARCHIVE_PATH         = 'build/rake.xcarchive'
EXPORT_PATH          = 'build/unity.ipa'
PROVISIONING_PROFILE = ENV['PROVISIONING_PROFILE']

def run(cmd)
  puts cmd
  `#{cmd}`
end

task :sync do
  `rsync -av Assets/Plugins/iOS/ Exports/iOS/`
  `rsync -av Assets/Plugins/Android/ Exports/Android/NegiParty/`
end

task :open do
  Dir.chdir PROJECT_DIR do
    `open #{PROJECT}`
  end
end

task :archive do
  Dir.chdir PROJECT_DIR do
    sh "xcodebuild", "-project", PROJECT, "-scheme", SCHEME, "-sdk", "iphoneos", "-configuration", "Release", "archive", "-archivePath", ARCHIVE_PATH
  end
end

task :export do
  Dir.chdir PROJECT_DIR do
    sh "xcodebuild", "-exportArchive", "-archivePath", ARCHIVE_PATH, "-exportPath", EXPORT_PATH, "-exportFormat", "ipa", "-exportProvisioningProfile", PROVISIONING_PROFILE
  end
end

task 'fabric:upload' do
  Dir.chdir PROJECT_DIR do
    sh CRASHLYTICS_SUBMIT, FABRIC_API_KEY, FABRIC_SECRET_KEY, "-emails", FABRIC_EMAIL, "-groupAliases", FABRIC_GROUP, "-ipaPath", EXPORT_PATH
  end
end

task :clean do
  Dir.chdir PROJECT_DIR do
    sh 'rm', IPA_PATH
  end
end

task :env do
  puts FABRIC_EMAIL
  puts FABRIC_GROUP
  puts PROVISIONING_PROFILE
end

